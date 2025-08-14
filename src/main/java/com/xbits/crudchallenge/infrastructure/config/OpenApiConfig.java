package com.xbits.crudchallenge.infrastructure.config;

import io.swagger.v3.oas.models.OpenAPI;
import io.swagger.v3.oas.models.info.Info;
import io.swagger.v3.oas.models.info.Contact;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration
public class OpenApiConfig {

    @Bean
    public OpenAPI customOpenAPI() {
        return new OpenAPI()
                .info(new Info()
                        .title("CRUD Produtos e Categorias API")
                        .version("1.0.0")
                        .description("API para gerenciamento de produtos e categorias - Desafio Xbits")
                        .contact(new Contact()
                                .name("Desenvolvedor")
                                .email("gabriel@dev.com")));
    }
}
