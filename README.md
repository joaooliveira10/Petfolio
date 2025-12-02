# Petfolio

API para cadastro e gerenciamento de pets, organizada em camadas (API, Application e Communication) e construída em .NET.


---

## Descrição Detalhada e Objetivos

O **Petfolio** é uma API REST para gerenciamento de pets (animais de estimação). Ela expõe endpoints para cadastrar, atualizar, listar, consultar e remover pets, utilizando uma organização em camadas que separa a API, os casos de uso (Application) e os contratos de comunicação (Communication).

**Objetivos principais:**

- Centralizar o cadastro de pets em uma API simples e extensível.
- Servir como base para estudos de arquitetura em camadas / Clean Architecture em .NET.
- Facilitar a integração com frontends, mobile apps ou ferramentas de automação.

**Escopo funcional detalhado:**

- Gerenciamento básico de pets (CRUD).
- Estrutura de código organizada em projetos separados por responsabilidade.


---

## Demonstrações

- Documentação interativa (ambiente de desenvolvimento):
  - Interface Scalar: `GET /api-docs`
  - Endpoint OpenAPI (padrão do ASP.NET): mapeado via `MapOpenApi()` em `Program.cs`.


---

## Funcionalidades

### Recursos de Pet

A API expõe os seguintes endpoints no `PetController` (`/api/pet`):

| Método | Rota            | Descrição                         | Request body        | Response principal                |
|--------|-----------------|-----------------------------------|---------------------|-----------------------------------|
| POST   | `/api/pet`      | Cadastra um novo pet             | `RequestPetJson`    | `ResponseRegisteredPetJson` (201) |
| PUT    | `/api/pet/{id}` | Atualiza um pet existente        | `RequestPetJson`    | 204 No Content                    |
| GET    | `/api/pet`      | Lista todos os pets              | -                   | `ResponseAllPetJson` (200/204)    |
| GET    | `/api/pet/{id}` | Busca um pet pelo identificador  | -                   | `ResponsePetJson` (200/404)       |
| DELETE | `/api/pet/{id}` | Remove um pet pelo identificador | -                   | 204 No Content                    |

### Regras de Negócio Visíveis

- Caso a listagem não encontre pets, a API retorna **204 No Content**.
- Para erros de validação ou regras de negócio, são utilizados DTOs de erro (`ResponseErrorsJson`).
- Regras específicas (limites, validações detalhadas, integrações externas) ainda **não estão claramente definidas** no código.

### O que **não** está implementado (visível no código)

- Autenticação e autorização.
- Persistência real em banco de dados (lógica de repositório não está visível). 
- Paginação, filtros e ordenação na listagem.

---

## Arquitetura e Tecnologias

### Visão Geral da Arquitetura

O projeto segue uma **arquitetura em camadas**, com clara separação de responsabilidades:

- **`Petfolio.API`**  
  Camada de apresentação (Web API), responsável por:
  - Configurar o host ASP.NET Core (`Program.cs`).
  - Mapear rotas e controllers (por exemplo, `PetController`).
  - Expor documentação OpenAPI/Scalar.

- **`Petfolio.Application`**  
  Camada de aplicação, concentrando regras de orquestração e casos de uso:
  - `UseCases/Pet/Register/RegisterPetUseCase.cs`
  - `UseCases/Pet/GetAll/GetAllPetsUseCase.cs`
  - `UseCases/Pet/GetById/GetByIdUseCase.cs`
  - `UseCases/Pet/Update/UpdatePetUseCase.cs`
  - `UseCases/Pet/Delete/DeletePetByIdUseCase.cs`

- **`Petfolio.Communication`**  
  Camada de contratos de comunicação (DTOs e enums):
  - `Enums/PetType.cs` (enumeração de tipos de pet).
  - `Requests/RequestPetJson.cs` (payload de entrada para operações de Pet).
  - `Responses/*.cs` (vários DTOs de resposta da API).



### Tecnologias Utilizadas

- **Plataforma:** .NET .  
- **Framework Web:** ASP.NET Core Web API.
- **Documentação da API:**
  - OpenAPI (via `builder.Services.AddOpenApi()`).
  - [Scalar](https://github.com/scalar/scalar) para visualização da documentação (`/api-docs`).
- **Outras bibliotecas/frameworks:**
  - Dependências padrão de ASP.NET Core.

---

## Instalação

### Pré-requisitos

- **SDK .NET:** Não informado .NET 10 .  
- **Git:** para clonagem do repositório.
- **Sistema Operacional:**
  - Desenvolvido/testado em: macOS.

### Passo a Passo

1. **Clonar o repositório**

   ```fish
   git clone <URL_DO_REPOSITORIO>
   cd Petfolio
   ```

2. **Restaurar dependências**

   ```fish
   dotnet restore
   ```

3. **Compilar a solução**

   ```fish
   dotnet build
   ```

4. **(Opcional) Rodar testes**

   ```fish
   dotnet test
   ```

> Caso algum comando falhe, verifique a versão do .NET instalada e as mensagens de erro exibidas.

---

## Configuração

Os principais arquivos de configuração ficam no projeto `Petfolio.API`:

- `Petfolio.API/appsettings.json`
- `Petfolio.API/appsettings.Development.json`
- `Petfolio.API/Properties/launchSettings.json`


## Como Usar

### Executar a API Localmente

Na raiz do repositório:

```fish
dotnet run --project Petfolio.API/Petfolio.API.csproj
```

Ou, em IDEs como Rider/Visual Studio, execute o profile configurado em `launchSettings.json`.

A URL base em ambiente local costuma ser algo como `https://localhost:<porta>` ou `http://localhost:<porta>`.  


### Principais Endpoints

Base URL: `http(s)://localhost:<porta>` (não informado).

- `POST /api/pet` – Cadastrar um novo pet.  
- `PUT /api/pet/{id}` – Atualizar um pet existente.  
- `GET /api/pet` – Listar todos os pets.  
- `GET /api/pet/{id}` – Buscar detalhes de um pet específico.  
- `DELETE /api/pet/{id}` – Remover um pet.

### Documentação Interativa

Com a API em execução em modo Development:

- Acesse `/api-docs` para abrir a interface Scalar.  
- Explore os endpoints, visualize o schema OpenAPI e teste requisições diretamente no navegador.

---

## Exemplos de Uso

### Exemplo de Cadastro de Pet (POST /api/pet)

**Request body (`RequestPetJson`):**

```json
{
  "name": "Bolinha",
  "birthday": "2020-05-10T00:00:00",
  "petType": 0
}
```

Onde:

- `name`: Nome do pet.  
- `birthday`: Data de nascimento do pet (formato ISO 8601).  
- `petType`: Valor inteiro correspondente ao enum `PetType` (por exemplo, 0 = Cat; consulte `PetType.cs`).

**Resposta de sucesso (`ResponseRegisteredPetJson`, status 201):**

```json
{
  "id": "<guid_do_pet>",
  "name": "Bolinha"
}
```

### Exemplo de Listagem de Pets (GET /api/pet)

**Resposta com pets (`ResponseAllPetJson`, status 200):**

```json
{
  "pets": [
    {
      "id": "<guid_1>",
      "name": "Bolinha",
      "petType": 0
    },
    {
      "id": "<guid_2>",
      "name": "Mel",
      "petType": 1
    }
  ]
}
```

**Quando não há registros:**  
Status **204 No Content** (sem corpo).

### Exemplo de Erro (formato `ResponseErrorsJson` – estrutura aproximada)

A estrutura exata de `ResponseErrorsJson` deve ser consultada em `Petfolio.Communication/Responses/ResponseErrorsJson.cs`.  
Como referência, um padrão comum é algo como:

```json
{
  "errors": [
    "Mensagem de erro 1",
    "Mensagem de erro 2"
  ]
}
```

> A estrutura real pode ser diferente. Este exemplo é apenas ilustrativo.

---

## Estrutura do Projeto

Resumo das principais pastas e responsabilidades:

```text
Petfolio/
├─ Petfolio.sln
├─ README.md
├─ Petfolio.API/
│  ├─ Program.cs
│  ├─ PetController.cs
│  ├─ appsettings.json
│  ├─ appsettings.Development.json
│  └─ Properties/
│     └─ launchSettings.json
├─ Petfolio.Application/
│  └─ UseCases/
│     └─ Pet/
│        ├─ Register/
│        ├─ GetAll/
│        ├─ GetById/
│        ├─ Update/
│        └─ Delete/
└─ Petfolio.Communication/
   ├─ Enums/
   │  └─ PetType.cs
   ├─ Requests/
   │  └─ RequestPetJson.cs
   └─ Responses/
      ├─ ResponseAllPetJson.cs
      ├─ ResponseErrorsJson.cs
      ├─ ResponsePetJson.cs
      ├─ ResponseRegisteredPetJson.cs
      └─ ResponseShortPetJson.cs
```

- **`Petfolio.API`**: entrada HTTP, controllers, configuração da aplicação.  
- **`Petfolio.Application`**: casos de uso e lógica de aplicação.  
- **`Petfolio.Communication`**: contratos expostos pela API (entrada e saída).


---



## Boas Práticas e Padrões Adotados

- **Organização em camadas:** separação entre API, Application e Communication.
- **Use Cases dedicados:** um caso de uso por operação de negócio (Register, GetAll, GetById, Update, Delete).
- **DTOs claros:** requests e responses separados em projeto próprio (`Petfolio.Communication`).


---

## Problemas Comuns e Soluções

| Problema                                         | Possível causa                                    | Sugestão de solução                                           |
|--------------------------------------------------|---------------------------------------------------|----------------------------------------------------------------|
| Erro de porta em uso ao subir a API              | Outra aplicação usando a mesma porta              | Ajustar a porta em `launchSettings.json` ou encerrar o outro processo. |
| Erros ao restaurar pacotes (`dotnet restore`)    | Falha de rede ou versão de SDK incompatível       | Verificar conexão, limpar cache de NuGet, conferir versão do SDK.      |
| Erros de certificado HTTPS em ambiente local     | Certificado de desenvolvimento não configurado    | Executar comandos padrão do ASP.NET para configurar certificados.      |
| `dotnet test` não encontra testes                | Não há projetos de teste configurados             | Criar projetos de teste (`*.Tests`) e adicionar à solução.             |



