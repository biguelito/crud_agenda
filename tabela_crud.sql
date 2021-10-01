create database crud;
use crud;

create table pessoa (
	idPessoa int AUTO_INCREMENT not null,
    nome VARCHAR(60) not null,
    email VARCHAR(45),
    telefone VARCHAR(13),
    PRIMARY KEY (idPessoa),
    UNIQUE INDEX(nome, email, telefone)
);

insert into pessoa (nome, email, telefone) values
('gabriel reis', 'gabrielr.nogueira2000@gmail.com', '81982039315'),
('luiz reis', '', '932546525'),
('edvania', 'edvania@gmail.com', '');

