# crud_agenda
CRUD de uma agenda para o processo seletivo da blue

O repositório contém o backend, o front end, e o script de criação do banco.
Porém, como o banco é local, é necessario trocar o id do usuario do banco e a senha desse usuario
para que seja possivel acessar a tabela

Essa troca pode ser feita no arquivo CRUDApi/appsettings.json
Sobrescrevendo a string "server=localhost;database=crud;user id={test};password={test@mysql}" com esses dados

Para rodar o back end entre na pasta crud_agenda e execute:

    cd CRUDApi
    dotnet run

Para rodar o front end entre na pasta crud_agenda e execute: 
    
    cd crudfront
    npm install
    npm run serve
