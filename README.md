# crud_agenda
CRUD de uma agenda para o processo seletivo da blue

O repositório contém o backend, o front end, e o script de criação do banco.
Porém, como o banco é local, é necessario trocar o id do usuario do banco e a senha desse usuario
para que seja possivel acessar a tabela

Essa troca pode ser feita no arquivo CRUDApi/appsettings.json
Sobrescrevendo a string "server=localhost;database=crud;user id={test};password={test@mysql}" com esses dados

Para rodar o projeto: 
    Back-end: "dotnet run" dentro da pasta CRUDApi
    Front-end: "npm install" e "npm run serve" dentro da pasta crudfront