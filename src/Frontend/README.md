# Frontend

Esta pasta contém o cliente da solução, a interface responsável pela interação entre todos os sistemas e o usuário final.

Um dos aspectos mais distintivos é a funcionalidade de IDE (Ambiente de Desenvolvimento Integrado) para a elaboração de terapias. Esta característica, baseada em programação em blocos, permite que os terapeutas ocupacionais estabeleçam a lógica para o momento da terapia com a criança. Em nosso caso, a programação se assemelha a montar peças de um quebra-cabeça, onde cada bloco de lógica pode se encaixar com outro, criando uma sequência lógica contínua.

Uma peça fundamental do projeto é o tapete, uma peça costurada de hardware que possui diversos quadrantes que podem ser programados para enviar um comando específico ao ser pressionado, clicado, etc. É através deste tapete que as crianças com deficiência interagem com o sistema. Por exemplo, os terapeutas podem estabelecer uma sequência de cliques no tapete que se correlaciona a uma ação, como tocar uma música, exibir uma imagem, etc. Para isso, disponibilizamos uma linguagem própria chamada QAL (Quick Assistive Language). O código QAL pode, então, ser compilado para o hardware do tapete, tornando a terapia personalizada e interativa.

O cliente foi desenvolvido utilizando o framework Next.js. Optamos pelo Next.js por sua eficiência na renderização do lado do servidor e geração de sites estáticos, que são essenciais para otimizar o desempenho da aplicação. Além disso, o Next.js oferece um sistema de rotas integrado que é simples de usar e permite um controle detalhado sobre as rotas da aplicação.

Para o design e estilização, escolhemos o Tailwind CSS. Este framework de CSS de utilidade-first nos permite criar designs personalizados de maneira eficaz diretamente no nosso HTML. Isso resulta em um código mais limpo e fácil de manter, enquanto nos proporciona a liberdade de criar uma interface de usuário única e atraente.

Para assegurar a qualidade do nosso código JavaScript, incorporamos o ESLint, uma ferramenta de linting de código aberto. Com o ESLint, podemos identificar e corrigir problemas no nosso código antes que eles se tornem questões mais graves. Isso aprimora a qualidade do nosso código e nos auxilia a manter um estilo de codificação consistente em todo o projeto.

# Sobre o Sistema

O "Portal do Terapeuta" é uma aplicação desenvolvida para a AACD (Associação de Assistência à Criança Deficiente). Ele é uma ferramenta dedicada aos terapeutas, projetada para facilitar a organização e a execução de suas tarefas diárias.

## Funcionalidades

1. **IDE para Criação de Terapias**: Esta funcionalidade permite que os terapeutas ocupacionais programem a lógica para o momento da terapia com a criança.

2. **Agenda**: Os terapeutas podem agendar suas sessões de terapia diretamente na aplicação. Ela permite a visualização de compromissos futuros e passados, bem como a adição, edição e exclusão de compromissos.

3. **Próximos pacientes**: Esta funcionalidade fornece uma visão geral dos pacientes que o terapeuta tem agendado para o dia atual.

4. **Estatísticas**: A aplicação fornece uma visão geral das estatísticas relacionadas tanto aos terapeutas quanto pacientes, como o número de terapias realizadas, a duração média das sessões, entre outros.

5. **Gerenciamento de Terapeutas**: Permite a criação, edição e exclusão de perfis de terapeutas.

6. **Gerenciamento de Pacientes**: Permite a criação, edição e exclusão de perfis de pacientes.

7. **Histórico de Terapias**: Os terapeutas podem visualizar o histórico de terapias realizadas, incluindo informações sobre o paciente, a duração da terapia, e outras informações relevantes.

8. **Credenciamento**: A aplicação possui um sistema de autenticação que permite aos terapeutas acessar suas informações de forma segura.

Estas funcionalidades foram projetadas para facilitar o trabalho dos terapeutas, permitindo que eles se concentrem no que é mais importante: o cuidado e a reabilitação de seus pacientes.

## Estrutura de Pastas

O projeto possui a seguinte estrutura de pastas:

```
.
├── .next
│   └── cache
├── app
│   ├── components
│   └── dashboard
│       ├── calendar
│       ├── metrics
│       ├── patients
│       ├── therapies
│       │   └── [id]
│       └── therapists
├── node_modules
└── public
```

- `.next`: Esta pasta é gerada automaticamente pelo Next.js e contém os arquivos compilados que serão usados em tempo de execução.

- `app`: Esta pasta contém toda a lógica principal da nossa aplicação. Dentro dela, temos:

  - `components`: Aqui estão todos os componentes React reutilizáveis em toda a aplicação.

  - `dashboard`: Esta pasta contém todas as páginas e componentes específicos do dashboard, como `calendar`, `metrics`, `patients`, `therapies` e `therapists`.

- `node_modules`: Esta pasta contém todos os pacotes do projeto instalados através do npm.

- `public`: Esta pasta contém todos os arquivos estáticos como imagens e ícones.

## Como rodar

Para rodar localmente, siga os passos abaixo:

1. Clone o repositório para sua máquina local usando `git clone git@github.com:2023M8T1Inteli/grupo5.git`

2. Mude para o diretório do projeto no terminal `cd src/Frontend`

3. Instale todas as dependências usando `npm install`

4. Inicie o servidor usando `npm run dev`

O aplicativo agora deve estar rodando em `http://localhost:3000`

## Visualização do Projeto

O design do projeto pode ser visualizado através do seguinte link no Figma: [Visualização - Portal do Terapeuta](https://www.figma.com/file/OtXLIAZedyOGiU2JwWigxv/AACD)
