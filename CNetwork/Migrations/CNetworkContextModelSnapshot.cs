using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CNetwork.Models;

namespace CNetwork.Migrations
{
    [DbContext(typeof(CNetworkContext))]
    partial class CNetworkContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("CNetwork.Models.Friends", b =>
                {
                    b.Property<int>("idFriends")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("UsersidUser");

                    b.Property<int>("idFriend");

                    b.Property<int>("idUser");

                    b.HasKey("idFriends");

                    b.HasIndex("UsersidUser");

                    b.HasIndex("idUser");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("CNetwork.Models.Invite", b =>
                {
                    b.Property<int>("InviteId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccepterId");

                    b.Property<int?>("FriendsidFriends");

                    b.Property<int>("RequesterId");

                    b.HasKey("InviteId");

                    b.HasIndex("AccepterId");

                    b.HasIndex("FriendsidFriends");

                    b.HasIndex("RequesterId");

                    b.ToTable("Invite");
                });

            modelBuilder.Entity("CNetwork.Models.Users", b =>
                {
                    b.Property<int>("idUser")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Description");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("idUser");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CNetwork.Models.Friends", b =>
                {
                    b.HasOne("CNetwork.Models.Users", "User")
                        .WithMany()
                        .HasForeignKey("UsersidUser");

                    b.HasOne("CNetwork.Models.Users", "Friend")
                        .WithMany("Friendship")
                        .HasForeignKey("idUser")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CNetwork.Models.Invite", b =>
                {
                    b.HasOne("CNetwork.Models.Users", "Accepter")
                        .WithMany("FriendAccept")
                        .HasForeignKey("AccepterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CNetwork.Models.Friends")
                        .WithMany("Invite")
                        .HasForeignKey("FriendsidFriends");

                    b.HasOne("CNetwork.Models.Users", "Requester")
                        .WithMany("FriendRequest")
                        .HasForeignKey("RequesterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
