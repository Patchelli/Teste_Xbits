package com.xbits.crudchallenge.infrastructure.repository;

import com.xbits.crudchallenge.domain.entity.Categoria;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface CategoriaRepository extends JpaRepository<Categoria, Long> {

    boolean existsByNome(String nome);

    boolean existsByNomeAndIdNot(String nome, Long id);

    Optional<Categoria> findByNome(String nome);
}
