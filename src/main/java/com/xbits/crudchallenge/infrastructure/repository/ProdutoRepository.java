package com.xbits.crudchallenge.infrastructure.repository;

import com.xbits.crudchallenge.domain.entity.Produto;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface ProdutoRepository extends JpaRepository<Produto, Long> {

    List<Produto> findByCategoriaId(Long categoriaId);

    @Query("SELECT p FROM Produto p JOIN FETCH p.categoria")
    List<Produto> findAllWithCategoria();

    @Query("SELECT p FROM Produto p JOIN FETCH p.categoria WHERE p.id = :id")
    Produto findByIdWithCategoria(@Param("id") Long id);
}
