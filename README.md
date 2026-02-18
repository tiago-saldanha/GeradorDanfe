# 🧾 GeradorDanfe

Aplicação web desenvolvida em **ASP.NET Core** para geração automática de **DANFE (NF-e e NFC-e)** a partir do upload de um arquivo XML.

O sistema identifica automaticamente o modelo do documento fiscal (55 ou 65), extrai a chave de acesso e gera o PDF correspondente.

---

## 🚀 Funcionalidades

- 📂 Upload de XML de NF-e ou NFC-e
- 🔎 Identificação automática do modelo:
  - **55** → NF-e
  - **65** → NFC-e
- 🔑 Extração automática da chave de acesso
- 📄 Geração do DANFE em PDF
- 🧠 Arquitetura baseada em serviços (SRP)
- 🧾 Nome do PDF baseado na chave da nota
- 🎨 Interface simples e amigável

---

## 🏗️ Arquitetura

O projeto segue boas práticas de separação de responsabilidades:

### 📌 Camadas

- **Controller**
  - Apenas orquestra requisições
  - Não contém regra de negócio

- **GeneratorService**
  - Responsável por:
    - Ler o XML
    - Identificar modelo
    - Delegar geração para o serviço correto
    - Extrair chave de acesso

- **INFeService / INFCeService**
  - Responsáveis pela geração específica do DANFE

---

## 🧠 Fluxo da Aplicação

1. Usuário envia XML
2. Sistema:
   - Lê conteúdo
   - Detecta `<mod>55</mod>` ou `<mod>65</mod>`
   - Extrai chave do atributo `Id` da tag `<infNFe>`
3. Serviço correspondente gera o PDF
4. Retorna o arquivo nomeado com a chave

---

## 🛠️ Tecnologias Utilizadas

- .NET 8
- ASP.NET Core MVC
- Dependency Injection
- LINQ to XML (`XDocument`)
- Bootstrap (UI)
- Logging com `ILogger`

---

## 📂 Estrutura Simplificada

```
GeradorDanfe.App
│
├── Controllers
│   └── HomeController
│
├── Services
│   ├── GeneratorService
│   ├── NFeService
│   └── NFCeService
│
├── Interfaces
│   ├── IGeneratorService
│   ├── INFeService
│   └── INFCeService
│
└── Models / DTOs
```

---

## 🔐 Tratamento de Erros

- XML inválido → `NotSupportedException`
- Modelo não suportado → exceção controlada
- Erros inesperados → log estruturado + mensagem amigável ao usuário

---

## 🧩 Registro de Dependências

```csharp
builder.Services.AddTransient<IGeneratorService, GeneratorService>();
builder.Services.AddTransient<INFeService, NFeService>();
builder.Services.AddTransient<INFCeService, NFCeService>();
```

---

## 📸 Preview

<img width="1919" height="941" alt="image" src="https://github.com/user-attachments/assets/b0aaed7f-84ae-411a-93a3-90473766aab9" />

<img width="1919" height="942" alt="image" src="https://github.com/user-attachments/assets/9ef39c9c-4725-4de7-b091-d4624b43ec0c" />

<img width="1245" height="789" alt="image" src="https://github.com/user-attachments/assets/c91b1561-73ba-4954-b614-5993bbae2b7f" />


---

## ▶️ Como Executar

1. Clone o repositório:

```bash
git clone https://github.com/seu-usuario/gerador-danfe-app.git
```

2. Acesse a pasta:

```bash
cd GeradorDanfe.App
```

3. Execute:

```bash
dotnet run
```

4. Abra no navegador:

```
https://localhost:5001
```

---

## 📌 Próximas Melhorias

- [ ] Validação estrutural do XML
- [ ] Testes unitários
- [ ] Dockerização
- [ ] Deploy em Azure
- [ ] Histórico de documentos gerados
- [ ] Upload múltiplo

---

## 🎯 Objetivo do Projeto

Este projeto foi desenvolvido com foco em:

- Aplicação prática de arquitetura em ASP.NET Core
- Uso correto de DI e lifetimes
- Separação clara de responsabilidades
- Código limpo e manutenível
- Projeto realista para portfólio

---

## 👨‍💻 Autor

Desenvolvido por **Tiago Ávila Saldanha**  
Projeto para estudo e portfólio profissional.

---

## 📄 Licença MIT

Este projeto é de uso educacional.
