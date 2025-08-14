package com.xbits.crudchallenge.application.dto;

public record TokenResponseDTO(
        String accessToken,
        String tokenType,
        Long expiresIn
) {}
