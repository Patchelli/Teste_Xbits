package com.xbits.crudchallenge.application.service;

import com.xbits.crudchallenge.application.dto.ProdutoRequestDTO;
import com.xbits.crudchallenge.application.dto.ProdutoResponseDTO;
import com.xbits.crudchallenge.domain.entity.Categoria;
import com.xbits.crudchallenge.domain.entity.Produto;
import com.xbits.crudchallenge.infrastructure.exception.ResourceNotFoundException;
import com.xbits.crudchallenge.infrastructure.repository.ProdutoRepository;
import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.stream.Collectors;

@Service
@Transactional
public class ProdutoService {

    @Autowired
    private ProdutoRepository produtoRepository;

    @Autowired
    private CategoriaService categoriaService;

    @Autowired
    private ModelMapper modelMapper;

    public ProdutoResponseDTO criar(ProdutoRequestDTO request) {
        Categoria categoria = categoriaService.buscarEntidadePorId(request.getCategoriaId());

        Produto produto = new Produto(
                request.getNome(),
                request.getDescricao(),
                request.getPreco(),
                categoria
        );

        produto = produtoRepository.save(produto);

        return modelMapper.map(produto, ProdutoResponseDTO.class);
    }

    @Transactional(readOnly = true)
    public List<ProdutoResponseDTO> listarTodos() {
        return produtoRepository.findAllWithCategoria()
                .stream()
                .map(produto -> modelMapper.map(produto, ProdutoResponseDTO.class))
                .collect(Collectors.toList());
    }

    @Transactional(readOnly = true)
    public ProdutoResponseDTO buscarPorId(Long id) {
        Produto produto = produtoRepository.findByIdWithCategoria(id);
        if (produto == null) {
            throw new ResourceNotFoundException("Produto não encontrado com ID: " + id);
        }

        return modelMapper.map(produto, ProdutoResponseDTO.class);
    }

    @Transactional(readOnly = true)
    public List<ProdutoResponseDTO> listarPorCategoria(Long categoriaId) {
        // Verifica se a categoria existe
        categoriaService.buscarEntidadePorId(categoriaId);

        return produtoRepository.findByCategoriaId(categoriaId)
                .stream()
                .map(produto -> modelMapper.map(produto, ProdutoResponseDTO.class))
                .collect(Collectors.toList());
    }

    public ProdutoResponseDTO atualizar(Long id, ProdutoRequestDTO request) {
        Produto produto = produtoRepository.findById(id)
                .orElseThrow(() -> new ResourceNotFoundException("Produto não encontrado com ID: " + id));

        Categoria categoria = categoriaService.buscarEntidadePorId(request.getCategoriaId());

        produto.setNome(request.getNome());
        produto.setDescricao(request.getDescricao());
        produto.setPreco(request.getPreco());
        produto.setCategoria(categoria);

        produto = produtoRepository.save(produto);

        return modelMapper.map(produto, ProdutoResponseDTO.class);
    }

    public void deletar(Long id) {
        if (!produtoRepository.existsById(id)) {
            throw new ResourceNotFoundException("Produto não encontrado com ID: " + id);
        }

        produtoRepository.deleteById(id);
    }
}
