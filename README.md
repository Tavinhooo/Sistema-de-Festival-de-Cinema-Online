# Sistema-de-Festival-de-Cinema-Online

## Estrutura por feature (ProjetoES.API)

- Controllers/
- Services/
- Repositories/
- Interfaces/
- Models/
- Factories/
- DTOs/

### Features normalizadas

- Admin
- Auth
- Acessos
- Avaliacoes
- Catalogos
- Compras
- Listas
- Sessoes
- Utilizadores

### Convencoes adotadas

- Nome de pasta DTOs normalizado para DTOs (em vez de DTOS).
- Nomes de feature alinhados entre layers para facilitar localizacao de ficheiros.
- Terminologia de feature normalizada para manter consistencia singular/plural entre dominios.

### Exemplo de navegacao

Para trabalhar numa feature (ex: Compras), os ficheiros ficam distribuidos por:

- ProjetoES.API/Controllers/Compras
- ProjetoES.API/Services/Compras
- ProjetoES.API/Repositories/Compras
- ProjetoES.API/DTOs/Compras
- ProjetoES.API/Models/Compras

Este padrao permite evoluir cada feature com menor acoplamento e melhor previsibilidade da estrutura.

### Mapa rapido de pastas

ProjetoES.API/Controllers

- Acessos
- Admin
- Auth
- Avaliacoes
- Catalogos
- Compras
- Listas
- Sessoes
- Utilizadores

ProjetoES.API/Services

- Acessos
- Admin
- Auth
- Avaliacoes
- Catalogos
- Compras
- Listas
- Sessoes
- Utilizadores

ProjetoES.API/Repositories

- Acessos
- Admin
- Auth
- Avaliacoes
- Catalogos
- Compras
- Listas
- Sessoes
- Utilizadores

ProjetoES.API/Interfaces

- Admin
- Avaliacoes
- Catalogos
- Listas

ProjetoES.API/DTOs

- Acessos
- Admin
- Auth
- Avaliacoes
- Catalogos
- Compras
- Sessoes
- Utilizadores

ProjetoES.API/Models

- Acessos
- Avaliacoes
- Catalogos
- Compras
- External
- Listas
- Sessoes
- Utilizadores

ProjetoES.API/Factories

- Acessos
- Listas
