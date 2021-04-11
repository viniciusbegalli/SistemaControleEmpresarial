﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaControleEmpresarial.Data;

namespace SistemaControleEmpresarial.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210408021716_Package04_2")]
    partial class Package04_2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.AjustePontoEletronico", b =>
                {
                    b.Property<int>("CodigoAjuste")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodigoUsuarioAprovador");

                    b.Property<DateTime>("DataAjuste");

                    b.Property<DateTime>("HoraPrimeiraEntrada");

                    b.Property<DateTime>("HoraPrimeiraSaida");

                    b.Property<DateTime>("HoraSegundaEntrada");

                    b.Property<DateTime>("HoraSegundaSaida");

                    b.Property<string>("Justificativa")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Observacoes")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("SituacaoAjuste");

                    b.Property<string>("UsuarioId");

                    b.HasKey("CodigoAjuste");

                    b.HasIndex("UsuarioId");

                    b.ToTable("AjustePontoEletronicos");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("CPF");

                    b.Property<int>("CodigoExterno");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NomeUsuario");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Telefone");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.Chamado", b =>
                {
                    b.Property<int>("CodigoChamado")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Fila")
                        .IsRequired();

                    b.Property<int>("IDPrioridade");

                    b.Property<string>("Prioridade")
                        .IsRequired();

                    b.Property<string>("SituacaoChamado");

                    b.Property<string>("Titulo")
                        .IsRequired();

                    b.Property<string>("UserId");

                    b.HasKey("CodigoChamado");

                    b.HasIndex("UserId");

                    b.ToTable("Chamados");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.ChamadoNota", b =>
                {
                    b.Property<int>("CodigoChamadoNota")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodigoChamado");

                    b.Property<int>("CodigoExterno");

                    b.Property<DateTime>("DataNota");

                    b.Property<string>("DescricaoNota")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("NomeUsuarioNota");

                    b.Property<string>("UserId");

                    b.HasKey("CodigoChamadoNota");

                    b.HasIndex("CodigoChamado");

                    b.HasIndex("UserId");

                    b.ToTable("ChamadoNotas");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.Feriado", b =>
                {
                    b.Property<int>("CodigoFeriado")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodigoUsuarioUltimaAtualizacao");

                    b.Property<DateTime>("Data");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<DateTime>("DataHoraUltimaAlteracao");

                    b.Property<string>("DescricaoFeriado")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("NomeUsuarioUltimaAtualizacao");

                    b.Property<string>("UserId");

                    b.HasKey("CodigoFeriado");

                    b.HasIndex("UserId");

                    b.ToTable("Feriados");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.LogAlteracao", b =>
                {
                    b.Property<int>("CodigoLogAlteracao")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodigoEntidade");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Entidade");

                    b.Property<string>("TipoOperacaoLog");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("CodigoLogAlteracao");

                    b.HasIndex("UserId");

                    b.ToTable("LogAlteracoes");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.LogAlteracaoItem", b =>
                {
                    b.Property<int>("CodigoLogAlteracaoItem")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodigoLogAlteracao");

                    b.Property<string>("Objeto");

                    b.Property<string>("ValorAntigo");

                    b.Property<string>("ValorNovo");

                    b.HasKey("CodigoLogAlteracaoItem");

                    b.HasIndex("CodigoLogAlteracao");

                    b.ToTable("LogAlteracaoItems");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.PontoEletronico", b =>
                {
                    b.Property<int>("CodigoPontoEletronico")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DataHoraPrimeiraEntrada");

                    b.Property<DateTime?>("DataHoraPrimeiraSaida");

                    b.Property<DateTime?>("DataHoraSegundaEntrada");

                    b.Property<DateTime?>("DataHoraSegundaSaida");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("CodigoPontoEletronico");

                    b.HasIndex("UserId");

                    b.ToTable("PontoEletronicos");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.SolicitacaoJornada", b =>
                {
                    b.Property<int>("CodigoSolicitacaoJornada")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodigoFeriado");

                    b.Property<int>("CodigoUsuarioAprovador");

                    b.Property<int>("CodigoUsuarioJornada");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<string>("Justificativa")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("NomeUsuarioJornada");

                    b.Property<string>("Observacoes")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("SituacaoSolicitacao");

                    b.Property<string>("UserId");

                    b.HasKey("CodigoSolicitacaoJornada");

                    b.HasIndex("CodigoFeriado");

                    b.HasIndex("UserId");

                    b.ToTable("SolicitacaoJornadas");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.Treinamento", b =>
                {
                    b.Property<int>("CodigoTreinamento")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataCriacao");

                    b.Property<DateTime>("DataFimTreinamento");

                    b.Property<DateTime>("DataInicioTreinamento");

                    b.Property<string>("Descricao")
                        .IsRequired();

                    b.Property<DateTime>("HoraFimTreinamento");

                    b.Property<DateTime>("HoraInicioTreinamento");

                    b.Property<string>("Observacoes")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("SituacaoTreinamento");

                    b.Property<string>("Titulo")
                        .IsRequired();

                    b.Property<string>("UserId");

                    b.HasKey("CodigoTreinamento");

                    b.HasIndex("UserId");

                    b.ToTable("Treinamentos");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.TreinamentoInstrutor", b =>
                {
                    b.Property<int>("CodigoTreinamentoInstrutor")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodigoExterno");

                    b.Property<int>("CodigoTreinamento");

                    b.Property<string>("NomeInstrutor");

                    b.Property<string>("UserId");

                    b.HasKey("CodigoTreinamentoInstrutor");

                    b.HasIndex("CodigoTreinamento");

                    b.HasIndex("UserId");

                    b.ToTable("TreinamentoInstrutores");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.Vaga", b =>
                {
                    b.Property<int>("CodigoVaga")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodigoUsuarioUltimaAtualizacao");

                    b.Property<DateTime>("DataCriacao");

                    b.Property<DateTime?>("DataHoraUltimaAlteracao");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("NomeUsuarioUltimaAtualizacao");

                    b.Property<string>("SituacaoVaga");

                    b.Property<string>("Titulo")
                        .IsRequired();

                    b.Property<string>("UserId");

                    b.HasKey("CodigoVaga");

                    b.HasIndex("UserId");

                    b.ToTable("Vagas");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.VagaCandidato", b =>
                {
                    b.Property<int>("CodigoVagaCandidato")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CodigoVaga");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("LinkedIn")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NomeCandidato")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Observacoes")
                        .HasMaxLength(100);

                    b.HasKey("CodigoVagaCandidato");

                    b.HasIndex("CodigoVaga");

                    b.ToTable("VagaCandidatos");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SistemaControleEmpresarial.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SistemaControleEmpresarial.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SistemaControleEmpresarial.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SistemaControleEmpresarial.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.AjustePontoEletronico", b =>
                {
                    b.HasOne("SistemaControleEmpresarial.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.Chamado", b =>
                {
                    b.HasOne("SistemaControleEmpresarial.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.ChamadoNota", b =>
                {
                    b.HasOne("SistemaControleEmpresarial.Models.Chamado", "Chamado")
                        .WithMany("Notas")
                        .HasForeignKey("CodigoChamado")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SistemaControleEmpresarial.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.Feriado", b =>
                {
                    b.HasOne("SistemaControleEmpresarial.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.LogAlteracao", b =>
                {
                    b.HasOne("SistemaControleEmpresarial.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.LogAlteracaoItem", b =>
                {
                    b.HasOne("SistemaControleEmpresarial.Models.LogAlteracao", "LogAlteracao")
                        .WithMany("Items")
                        .HasForeignKey("CodigoLogAlteracao")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.PontoEletronico", b =>
                {
                    b.HasOne("SistemaControleEmpresarial.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.SolicitacaoJornada", b =>
                {
                    b.HasOne("SistemaControleEmpresarial.Models.Feriado", "Feriado")
                        .WithMany()
                        .HasForeignKey("CodigoFeriado")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SistemaControleEmpresarial.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.Treinamento", b =>
                {
                    b.HasOne("SistemaControleEmpresarial.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.TreinamentoInstrutor", b =>
                {
                    b.HasOne("SistemaControleEmpresarial.Models.Treinamento", "Treinamento")
                        .WithMany("InstrutoresTreinamento")
                        .HasForeignKey("CodigoTreinamento")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SistemaControleEmpresarial.Models.ApplicationUser", "Instrutor")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.Vaga", b =>
                {
                    b.HasOne("SistemaControleEmpresarial.Models.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SistemaControleEmpresarial.Models.VagaCandidato", b =>
                {
                    b.HasOne("SistemaControleEmpresarial.Models.Vaga", "Vaga")
                        .WithMany()
                        .HasForeignKey("CodigoVaga")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
