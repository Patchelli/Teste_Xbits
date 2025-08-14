package com.xbits.crudchallenge.presentation.controller;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.xbits.crudchallenge.application.dto.CategoriaRequestDTO;
import com.xbits.crudchallenge.application.dto.CategoriaResponseDTO;
import com.xbits.crudchallenge.application.service.CategoriaService;
import com.xbits.crudchallenge.infrastructure.exception.BusinessException;
import com.xbits.crudchallenge.infrastructure.exception.ResourceNotFoundException;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.autoconfigure.web.servlet.WebMvcTest;
import org.springframework.http.MediaType;
import org.springframework.test.context.bean.override.mockito.MockitoBean;
import org.springframework.test.web.servlet.MockMvc;

import java.util.Arrays;
import java.util.List;

import static org.mockito.ArgumentMatchers.*;
import static org.mockito.Mockito.*;
import static org.springframework.test.web.servlet.request.MockMvcRequestBuilders.*;
import static org.springframework.test.web.servlet.result.MockMvcResultMatchers.*;

@WebMvcTest(CategoriaController.class)
class CategoriaControllerTest {

    @Autowired
    private MockMvc mockMvc;

    @MockitoBean
    private CategoriaService categoriaService;

    @Autowired
    private ObjectMapper objectMapper;

    private CategoriaRequestDTO categoriaRequest;
    private CategoriaResponseDTO categoriaResponse;

    @BeforeEach
    void setUp() {
        categoriaRequest = new CategoriaRequestDTO("Eletrônicos", "Produtos eletrônicos");
        categoriaResponse = new CategoriaResponseDTO();
        categoriaResponse.setId(1L);
        categoriaResponse.setNome("Eletrônicos");
        categoriaResponse.setDescricao("Produtos eletrônicos");
    }

    @Test
    void deveCriarCategoriaComSucesso() throws Exception {
        // Given
        when(categoriaService.criar(any(CategoriaRequestDTO.class))).thenReturn(categoriaResponse);

        // When & Then
        mockMvc.perform(post("/api/categorias")
                        .contentType(MediaType.APPLICATION_JSON)
                        .content(objectMapper.writeValueAsString(categoriaRequest)))
                .andExpect(status().isCreated())
                .andExpect(jsonPath("$.id").value(1L))
                .andExpect(jsonPath("$.nome").value("Eletrônicos"));
    }

    @Test
    void deveRetornarBadRequestParaCategoriaInvalida() throws Exception {
        // Given
        CategoriaRequestDTO requestInvalido = new CategoriaRequestDTO("", "Descrição");

        // When & Then
        mockMvc.perform(post("/api/categorias")
                        .contentType(MediaType.APPLICATION_JSON)
                        .content(objectMapper.writeValueAsString(requestInvalido)))
                .andExpect(status().isBadRequest());
    }

    @Test
    void deveRetornarConflictParaNomeDuplicado() throws Exception {
        // Given
        when(categoriaService.criar(any(CategoriaRequestDTO.class)))
                .thenThrow(new BusinessException("Já existe uma categoria com este nome"));

        // When & Then
        mockMvc.perform(post("/api/categorias")
                        .contentType(MediaType.APPLICATION_JSON)
                        .content(objectMapper.writeValueAsString(categoriaRequest)))
                .andExpect(status().isConflict())
                .andExpect(jsonPath("$.message").value("Já existe uma categoria com este nome"));
    }

    @Test
    void deveListarTodasAsCategorias() throws Exception {
        // Given
        List<CategoriaResponseDTO> categorias = Arrays.asList(categoriaResponse);
        when(categoriaService.listarTodas()).thenReturn(categorias);

        // When & Then
        mockMvc.perform(get("/api/categorias"))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$").isArray())
                .andExpect(jsonPath("$[0].nome").value("Eletrônicos"));
    }

    @Test
    void deveBuscarCategoriaPorId() throws Exception {
        // Given
        when(categoriaService.buscarPorId(1L)).thenReturn(categoriaResponse);

        // When & Then
        mockMvc.perform(get("/api/categorias/1"))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.id").value(1L))
                .andExpect(jsonPath("$.nome").value("Eletrônicos"));
    }

    @Test
    void deveRetornarNotFoundParaCategoriaInexistente() throws Exception {
        // Given
        when(categoriaService.buscarPorId(1L))
                .thenThrow(new ResourceNotFoundException("Categoria não encontrada com ID: 1"));

        // When & Then
        mockMvc.perform(get("/api/categorias/1"))
                .andExpect(status().isNotFound())
                .andExpect(jsonPath("$.message").value("Categoria não encontrada com ID: 1"));
    }

    @Test
    void deveAtualizarCategoriaComSucesso() throws Exception {
        // Given
        when(categoriaService.atualizar(eq(1L), any(CategoriaRequestDTO.class))).thenReturn(categoriaResponse);

        // When & Then
        mockMvc.perform(put("/api/categorias/1")
                        .contentType(MediaType.APPLICATION_JSON)
                        .content(objectMapper.writeValueAsString(categoriaRequest)))
                .andExpect(status().isOk())
                .andExpect(jsonPath("$.nome").value("Eletrônicos"));
    }

    @Test
    void deveDeletarCategoriaComSucesso() throws Exception {
        // Given
        doNothing().when(categoriaService).deletar(1L);

        // When & Then
        mockMvc.perform(delete("/api/categorias/1"))
                .andExpect(status().isNoContent());
    }
}
