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
- Bootstrap (UI)
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

<img width="1901" height="942" alt="image" src="https://github.com/user-attachments/assets/02d4ec31-ad78-477f-9105-2b833e3c1034" />

<img width="1024" height="1024" alt="Danfe" src="https://github.com/user-attachments/assets/0d3b1065-fe0a-4e85-8c4d-25a8bb242f71" />

---

## ▶️ Como Executar

1. Clone o repositório:

```bash
git clone https://github.com/seu-usuario/gerador-danfe-app.git
```

2. Acesse a pasta:

```bash
cd src/GeradorDanfe.Web
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

- [ ] Testes unitários
- [ ] Deploy em Azure
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
