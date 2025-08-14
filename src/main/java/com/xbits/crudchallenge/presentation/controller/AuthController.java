package com.xbits.crudchallenge.presentation.controller;

import com.xbits.crudchallenge.application.dto.LoginRequestDTO;
import com.xbits.crudchallenge.application.dto.TokenResponseDTO;
import com.xbits.crudchallenge.infrastructure.security.JwtUtil;
import io.swagger.v3.oas.annotations.Operation;
import io.swagger.v3.oas.annotations.tags.Tag;
import jakarta.validation.Valid;
import org.springframework.http.ResponseEntity;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/auth")
@Tag(name = "Autenticação", description = "Endpoints para autenticação JWT")
public class AuthController {

    private final JwtUtil jwtUtil;

    private static final String DEMO_USERNAME = "admin";
    private static final String DEMO_PASSWORD = "admin123";

    public AuthController(JwtUtil jwtUtil, PasswordEncoder passwordEncoder) {
        this.jwtUtil = jwtUtil;
    }

    @PostMapping("/login")
    @Operation(summary = "Realizar login", description = "Autentica usuário e retorna token JWT")
    public ResponseEntity<TokenResponseDTO> login(@Valid @RequestBody LoginRequestDTO request) {
        if (DEMO_USERNAME.equals(request.username()) && DEMO_PASSWORD.equals(request.password())) {
            String token = jwtUtil.generateToken(request.username());
            return ResponseEntity.ok(new TokenResponseDTO(token, "Bearer", 86400L));
        }

        return ResponseEntity.status(401).build();
    }
}
