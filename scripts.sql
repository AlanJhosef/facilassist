/*
	drop table usuarios
	drop table cliente
	drop table Situacao

	drop procedure AprovarReprovarCliente 
	drop procedure ListarCliente 
	drop procedure AlterarCliente
	drop procedure ExcluirCliente 
	drop procedure InserirCliente 
	drop procedure ListarUsuario 
*/



create table usuarios(
	usuarioId varchar(10) primary key not null,	
	Senha varchar(40)
);
GO
insert into usuarios
select 'admin','123'

create table cliente(
	Id int primary key not null identity,
	Nome varchar(100)  not null,
	CPF varchar(11) not null,
	DataNascimento datetime  not null,
	Sexo char(1),
	SituacaoId int not null,	
	DataCriacao datetime not null default(getdate()),
	UsuarioCriacao varchar(10) not null default(host_name()),
	DataAlteracao datetime not null default(getdate()),
	UsuarioAlteracao varchar(10) not null default(host_name()),
);
GO
create table Situacao(
	Id int primary key not null identity,	
	Situacao varchar(10)
);
GO

ALTER TABLE cliente add FOREIGN KEY (SituacaoId) REFERENCES Situacao(Id)
GO

insert into Situacao(Situacao)
select 'Em Analise'
insert into Situacao(Situacao)
select 'Aprovado'
insert into Situacao(Situacao)
select 'Reprovado' 
GO

create procedure InserirCliente(
	@Nome varchar(100),
	@CPF varchar(11),
	@DataNascimento datetime,
	@Sexo char(1),
	@UsuarioCriacao varchar(10)
)as

-- sempre incluir o lciente no status em analise (select * from Situacao)
declare @SituacaoId int = 1, @erro varchar(max)  = ''

if exists(select '' from cliente where cpf = @CPF)
begin
	set @erro = 'Cliente já cadastrado.'
	goto Error
end

insert into cliente(
Nome
,CPF
,DataNascimento
,Sexo
,SituacaoId
,DataCriacao
,UsuarioCriacao
,DataAlteracao
,UsuarioAlteracao
)
select 
@Nome
,@CPF
,@DataNascimento
,@Sexo
,@SituacaoId
,getdate()
,@UsuarioCriacao
,getdate()
-- para alteração vou deixar o mesmo usuario que criou
,@UsuarioCriacao

return
Error:
	raiserror(@erro, 11, 1)
GO


create or alter procedure ListarCliente(
	@Id int
)as
 

select
c.Id
,c.Nome
,c.CPF
,c.DataNascimento
,c.Sexo
,c.SituacaoId
,c.DataCriacao
,c.UsuarioCriacao
,c.DataAlteracao
,c.UsuarioAlteracao
,s.Situacao
from cliente c
join situacao s on s.Id = c.SituacaoId
where c.Id = @Id or isnull(@Id,0) = 0
 
GO

create procedure ExcluirCliente(
	@Id int
) as

delete 
from cliente where Id = @Id 
 
GO


create procedure AlterarCliente(
	@Id int,
	@Nome varchar(100),
	@CPF varchar(11),
	@DataNascimento datetime,
	@Sexo char(1),
	@SituacaoId int,
	@Usuario varchar(10)
) as
 

update cliente
set Nome = @Nome , DataNascimento = @DataNascimento, Sexo = @Sexo, SituacaoId = @SituacaoId, DataAlteracao = getdate(),  UsuarioAlteracao = @Usuario
where Id = @Id
 

return
 
GO

create procedure ListarUsuario(
	@UsuarioId varchar(10),
	@Senha varchar(40)
) as
 
 declare @msgErro varchar(100)

select u.usuarioId
from usuarios u 
where u.usuarioId = @UsuarioId
and u.Senha = @Senha 

return
 
GO


create procedure AprovarReprovarCliente(
	@Id int,
	@SituacaoId int,
	@usuario varchar(10)
) as
 
declare @msgErro varchar(100)

update cliente
set SituacaoId = @SituacaoId, DataAlteracao = getdate(), UsuarioAlteracao = @usuario	
where Id = @Id

--todo: criar historico 

return
 
GO
 