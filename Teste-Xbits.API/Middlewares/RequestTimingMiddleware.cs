using System.Diagnostics;

namespace TimeSheet.API.Middlewares;

public class RequestTimingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestTimingMiddleware> _logger;

    public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        long responseSize = 0;
        
        var originalBody = context.Response.Body;
        using var countingStream = new CountingStream(originalBody);
        context.Response.Body = countingStream;

        try
        {
            await _next(context);
        }
        finally
        {
            context.Response.Body = originalBody;
            responseSize = countingStream.BytesWritten;
            stopwatch.Stop();
            
            _logger.LogInformation(
                " Endpoint: {Method} {Path} | Tempo Total: {ElapsedMs} ms | Tamanho Resposta: {Size} bytes",
                context.Request.Method,
                context.Request.Path,
                stopwatch.ElapsedMilliseconds,
                responseSize
            );
        }
    }
}

public class CountingStream : Stream
{
    private readonly Stream _innerStream;
    public long BytesWritten { get; private set; }

    public CountingStream(Stream innerStream)
    {
        _innerStream = innerStream;
    }

    public override bool CanRead => _innerStream.CanRead;
    public override bool CanSeek => _innerStream.CanSeek;
    public override bool CanWrite => _innerStream.CanWrite;
    public override long Length => _innerStream.Length;
    public override long Position
    {
        get => _innerStream.Position;
        set => _innerStream.Position = value;
    }

    public override void Flush() => _innerStream.Flush();
    public override Task FlushAsync(CancellationToken cancellationToken) => _innerStream.FlushAsync(cancellationToken);
    public override int Read(byte[] buffer, int offset, int count) => _innerStream.Read(buffer, offset, count);
    public override long Seek(long offset, SeekOrigin origin) => _innerStream.Seek(offset, origin);
    public override void SetLength(long value) => _innerStream.SetLength(value);

    public override void Write(byte[] buffer, int offset, int count)
    {
        BytesWritten += count;
        _innerStream.Write(buffer, offset, count);
    }

    public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        BytesWritten += count;
        await _innerStream.WriteAsync(buffer, offset, count, cancellationToken);
    }

    public override async ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
    {
        BytesWritten += buffer.Length;
        await _innerStream.WriteAsync(buffer, cancellationToken);
    }
}