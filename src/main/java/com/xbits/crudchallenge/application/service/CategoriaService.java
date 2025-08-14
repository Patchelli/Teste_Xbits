package com.xbits.crudchallenge.application.service;

import com.xbits.crudchallenge.application.dto.CategoriaRequestDTO;
import com.xbits.crudchallenge.application.dto.CategoriaResponseDTO;
import com.xbits.crudchallenge.domain.entity.Categoria;
import com.xbits.crudchallenge.infrastructure.exception.BusinessException;
import com.xbits.crudchallenge.infrastructure.exception.ResourceNotFoundException;
import com.xbits.crudchallenge.infrastructure.repository.CategoriaRepository;
import org.modelmapper.ModelMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.stream.Collectors;

    @Service
    @Transactional
    public class CategoriaService {

        @Autowired
        private CategoriaRepository categoriaRepository;

        @Autowired
        private ModelMapper modelMapper;

        public CategoriaResponseDTO criar(CategoriaRequestDTO request) {
            if (categoriaRepository.existsByNome(request.getNome())) {
                throw new BusinessException("Já existe uma categoria com este nome");
            }

            Categoria categoria = new Categoria(request.getNome(), request.getDescricao());
            categoria = categoriaRepository.save(categoria);

            return modelMapper.map(categoria, CategoriaResponseDTO.class);
        }

        @Transactional(readOnly = true)
        public List<CategoriaResponseDTO> listarTodas() {
            return categoriaRepository.findAll()
                    .stream()
                    .map(categoria -> modelMapper.map(categoria, CategoriaResponseDTO.class))
                    .collect(Collectors.toList());
        }

        @Transactional(readOnly = true)
        public CategoriaResponseDTO buscarPorId(Long id) {
            Categoria categoria = categoriaRepository.findById(id)
                    .orElseThrow(() -> new ResourceNotFoundException("Categoria não encontrada com ID: " + id));

            return modelMapper.map(categoria, CategoriaResponseDTO.class);
        }

        public CategoriaResponseDTO atualizar(Long id, CategoriaRequestDTO request) {
            Categoria categoria = categoriaRepository.findById(id)
                    .orElseThrow(() -> new ResourceNotFoundException("Categoria não encontrada com ID: " + id));

            if (categoriaRepository.existsByNomeAndIdNot(request.getNome(), id)) {
                throw new BusinessException("Já existe uma categoria com este nome");
            }

            categoria.setNome(request.getNome());
            categoria.setDescricao(request.getDescricao());
            categoria = categoriaRepository.save(categoria);

            return modelMapper.map(categoria, CategoriaResponseDTO.class);
        }

        public void deletar(Long id) {
            if (!categoriaRepository.existsById(id)) {
                throw new ResourceNotFoundException("Categoria não encontrada com ID: " + id);
            }

            categoriaRepository.deleteById(id);
        }

        @Transactional(readOnly = true)
        public Categoria buscarEntidadePorId(Long id) {
            return categoriaRepository.findById(id)
                    .orElseThrow(() -> new ResourceNotFoundException("Categoria não encontrada com ID: " + id));
        }
    }

