# calculadora_teste

# 🧮 Calculadora Web – ASP.NET Core MVC

Aplicação web desenvolvida em **ASP.NET Core MVC** como parte do processo seletivo (Etapa 2), consistindo em uma **calculadora com persistência de dados via API REST**.

O projeto foi desenvolvido com foco em **organização de código, boas práticas, validações, experiência do usuário e arquitetura limpa**.

---

## 🚀 Funcionalidades

### ➕ Calculadora
- Entrada de **Valor A** e **Valor B**
- Operações matemáticas:
  - Soma (+)
  - Subtração (-)
  - Multiplicação (*)
  - Divisão (/)
- Validações:
  - Campos obrigatórios
  - Apenas números válidos
  - Bloqueio de divisão por zero
- Mensagem de sucesso após gravação:

- Limpeza automática dos campos após o cálculo

---

### 📊 Histórico de Cálculos
- Listagem em tabela de todos os cálculos armazenados via API
- Exibição formatada:
- Valores com **2 casas decimais**
- Data no formato **dd/MM/yyyy HH:mm:ss**
- Exclusão de registros pelo **ID**

---

## 🏗️ Arquitetura

O projeto segue o padrão **MVC (Model-View-Controller)** com separação clara de responsabilidades:


