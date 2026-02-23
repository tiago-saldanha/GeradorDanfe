# 🧾 GeradorDanfe

Aplicação web desenvolvida em **ASP.NET Core** para geração automática de **DANFE (NF-e)** a partir do upload de um arquivo XML.

O sistema identifica automaticamente o documento fiscal, extrai a chave de acesso e gera o PDF correspondente utilizando **DinkToPdf (wkhtmltopdf)**.

---

## 🚀 Funcionalidades

- 📂 Upload de XML de NF-e
- 🔑 Extração automática da chave de acesso
- 📄 Geração do DANFE em PDF
- 🧠 Arquitetura baseada em serviços (SRP)
- 🧾 Nome do PDF baseado na chave da nota
- 🎨 Interface simples e amigável
- 🖥️ Compatível com Windows e Linux

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
    - Delegar geração para o serviço correto
    - Extrair chave de acesso

- **INFeService**
  - Responsáveis pela geração específica do DANFE
- **IPDFService**
  - Responsável por gerar o PDF utilizando DinkToPdf

---

## 🧠 Fluxo da Aplicação

1. Usuário envia XML
2. Sistema:
   - Lê conteúdo
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
- DinkToPdf
- wkhtmltopdf

---

📄 Geração de PDF (DinkToPdf + wkhtmltox)

A aplicação utiliza DinkToPdf, que é um wrapper .NET para o wkhtmltopdf.

🪟 Windows
- A biblioteca `libwkhtmltox.dll` já está incluída no projeto:

```bash
/Lib/libwkhtmltox.dll
```
Mesmo assim, recomenda-se instalar o wkhtmltopdf oficialmente na máquina para evitar problemas de dependências.

[Download oficial](https://wkhtmltopdf.org/downloads.html)

---

🐧 Linux (Ubuntu exemplo)

No Linux é necessário instalar o pacote do wkhtmltox.

Repositório oficial de pacotes:
https://github.com/wkhtmltopdf/packaging/releases

Exemplo para Ubuntu:

```bash
sudo dpkg -i wkhtmltox_{{versao}}.deb
```

Caso falte alguma dependência:
```bash
sudo apt install -f
```

Para verificar se a biblioteca está instalada:
```bash
whereis libwkhtmltox.so
```

Normalmente será instalada em:
```bash
/usr/local/lib/libwkhtmltox.so
```

No Linux, o sistema operacional gerencia o binário instalado globalmente, não sendo necessário incluir o arquivo manualmente dentro do projeto.

---

## 🔐 Tratamento de Erros

- XML inválido → `NotSupportedException`
- Modelo não suportado → exceção controlada
- Erros inesperados → log estruturado + mensagem amigável ao usuário

---

## 📸 Preview

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
