package com.xbits.crudchallenge.application.service;

import com.xbits.crudchallenge.application.dto.CategoriaRequestDTO;
import com.xbits.crudchallenge.application.dto.CategoriaResponseDTO;
import com.xbits.crudchallenge.domain.entity.Categoria;
import com.xbits.crudchallenge.infrastructure.exception.BusinessException;
import com.xbits.crudchallenge.infrastructure.exception.ResourceNotFoundException;
import com.xbits.crudchallenge.infrastructure.repository.CategoriaRepository;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.junit.jupiter.MockitoExtension;
import org.modelmapper.ModelMapper;

import java.util.Arrays;
import java.util.List;
import java.util.Optional;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.ArgumentMatchers.*;
import static org.mockito.Mockito.*;

@ExtendWith(MockitoExtension.class)
class CategoriaServiceTest {

    @Mock
    private CategoriaRepository categoriaRepository;

    @Mock
    private ModelMapper modelMapper;

    @InjectMocks
    private CategoriaService categoriaService;

    private CategoriaRequestDTO categoriaRequest;
    private Categoria categoria;
    private CategoriaResponseDTO categoriaResponse;

    @BeforeEach
    void setUp() {
        lenient().when(modelMapper.map(any(Categoria.class), eq(CategoriaResponseDTO.class)))
                .thenAnswer(invocation -> {
                    Categoria cat = invocation.getArgument(0);
                    CategoriaResponseDTO dto = new CategoriaResponseDTO();
                    dto.setId(cat.getId());
                    dto.setNome(cat.getNome());
                    dto.setDescricao(cat.getDescricao());
                    return dto;
                });

        categoriaRequest = new CategoriaRequestDTO("Eletrônicos", "Produtos eletrônicos");
        categoria = new Categoria("Eletrônicos", "Produtos eletrônicos");
        categoria.setId(1L);
        categoriaResponse = new CategoriaResponseDTO();
        categoriaResponse.setId(1L);
        categoriaResponse.setNome("Eletrônicos");
        categoriaResponse.setDescricao("Produtos eletrônicos");
    }

    @Test
    void deveCriarCategoriaComSucesso() {
        // Given
        when(categoriaRepository.existsByNome(categoriaRequest.getNome())).thenReturn(false);
        when(categoriaRepository.save(any(Categoria.class))).thenReturn(categoria);

        // When
        CategoriaResponseDTO result = categoriaService.criar(categoriaRequest);

        // Then
        assertNotNull(result);
        assertEquals("Eletrônicos", result.getNome());
        verify(categoriaRepository).existsByNome("Eletrônicos");
        verify(categoriaRepository).save(any(Categoria.class));
    }

    @Test
    void deveLancarExcecaoAoCriarCategoriaComNomeExistente() {
        // Given
        when(categoriaRepository.existsByNome(categoriaRequest.getNome())).thenReturn(true);

        // When & Then
        BusinessException exception = assertThrows(BusinessException.class,
                () -> categoriaService.criar(categoriaRequest));

        assertEquals("Já existe uma categoria com este nome", exception.getMessage());
        verify(categoriaRepository, never()).save(any(Categoria.class));
    }

    @Test
    void deveListarTodasAsCategorias() {
        // Given
        List<Categoria> categorias = Arrays.asList(categoria);
        when(categoriaRepository.findAll()).thenReturn(categorias);

        // When
        List<CategoriaResponseDTO> result = categoriaService.listarTodas();

        // Then
        assertNotNull(result);
        assertEquals(1, result.size());
        assertEquals("Eletrônicos", result.get(0).getNome());
    }

    @Test
    void deveBuscarCategoriaPorId() {
        // Given
        when(categoriaRepository.findById(1L)).thenReturn(Optional.of(categoria));

        // When
        CategoriaResponseDTO result = categoriaService.buscarPorId(1L);

        // Then
        assertNotNull(result);
        assertEquals("Eletrônicos", result.getNome());
    }

    @Test
    void deveLancarExcecaoAoBuscarCategoriaInexistente() {
        // Given
        when(categoriaRepository.findById(1L)).thenReturn(Optional.empty());

        // When & Then
        ResourceNotFoundException exception = assertThrows(ResourceNotFoundException.class,
                () -> categoriaService.buscarPorId(1L));

        assertEquals("Categoria não encontrada com ID: 1", exception.getMessage());
    }

    @Test
    void deveAtualizarCategoriaComSucesso() {
        // Given
        when(categoriaRepository.findById(1L)).thenReturn(Optional.of(categoria));
        when(categoriaRepository.existsByNomeAndIdNot("Eletrônicos", 1L)).thenReturn(false);
        when(categoriaRepository.save(categoria)).thenReturn(categoria);

        // When
        CategoriaResponseDTO result = categoriaService.atualizar(1L, categoriaRequest);

        // Then
        assertNotNull(result);
        assertEquals("Eletrônicos", result.getNome());
        verify(categoriaRepository).save(categoria);
    }

    @Test
    void deveDeletarCategoriaComSucesso() {
        // Given
        when(categoriaRepository.existsById(1L)).thenReturn(true);

        // When
        categoriaService.deletar(1L);

        // Then
        verify(categoriaRepository).deleteById(1L);
    }

    @Test
    void deveLancarExcecaoAoDeletarCategoriaInexistente() {
        // Given
        when(categoriaRepository.existsById(1L)).thenReturn(false);

        // When & Then
        ResourceNotFoundException exception = assertThrows(ResourceNotFoundException.class,
                () -> categoriaService.deletar(1L));

        assertEquals("Categoria não encontrada com ID: 1", exception.getMessage());
        verify(categoriaRepository, never()).deleteById(1L);
    }
}
