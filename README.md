Cadastro de Cliente .NET


Repositorio para guardar os dois projetos (front e back) de um cadastro simples de cliente em .net framework e .net 8.

Utilizando um mix de ferramentas/frameworks simples para fazer um cadastro/edição/exclusão/listagem/aprovação e reprovação de um cliente.

O fluxo é simples, o cadastro do cliente é feito, vai para uma tela onde vai ter a lista de clientes e em cada cliente é possível editar, aprovar ou reprovar.

No editar, é simples vai trazer os dados com jquery e para editar tambem.

Aprovar e reprovar a mesma coisa, usando jquery.

E quando abre a tela do editar pode também excluir o cliente.

Para testar:

- checar caminho de api no projeto front.
- na api checar a conexão com o banco de dados.
- Necessário .net8, .netframework 4.7 e SQL SERVER.

FRONT

No Front usamos .net framework 4.7 + jquery + bootstrap, para facilitar o uso de consultas no back e estilo e posição de elementos. 

Como é um projeto simples preferi fazer com o .net framework mesmo apenas para mostrar conhecimento em um framework que esta caindo em desuso. 

BACK

Na api estamos usando .net8, não escolhi o .net9 pela familiaridade com algumas libs que sei que já funciona no .net8 

Separei em um unico projeto api / service / data. Como é um projeto de teste e pequeno achei melhor não separar as camadas como outros projetos.

Aqui estamos usando a autenticação JWT que é bem simples e seguro.

Dapper para recuperar o dataset do banco de dados, facilita muito o uso e manutenção. 

Fluent Validation, para facilitar a validação de alguns dados logo na entrada da chamdada e deixar também o código mais limpo. 

