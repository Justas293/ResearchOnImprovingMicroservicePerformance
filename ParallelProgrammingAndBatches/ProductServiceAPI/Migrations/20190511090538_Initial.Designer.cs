﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductServiceAPI.DbContexts;

namespace ProductServiceAPI.Migrations
{
    [DbContext(typeof(ProductContext))]
    [Migration("20190511090538_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProductServiceAPI.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new { Id = 1, Description = "Electronic Items", Name = "Electronics" },
                        new { Id = 2, Description = "Dresses", Name = "Clothes" },
                        new { Id = 3, Description = "Grocery Items", Name = "Grocery" }
                    );
                });

            modelBuilder.Entity("ProductServiceAPI.Models.Product", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new { Id = "69a6d3a9-8d1b-428c-b6bc-50dd483522a7", CategoryId = 1, Description = "Product_0 description", Name = "Product_0", Price = 10m },
                        new { Id = "2e4cdeb7-dbd8-489e-8bf5-c260d2c0e0bb", CategoryId = 2, Description = "Product_1 description", Name = "Product_1", Price = 11m },
                        new { Id = "daa1ddf2-f1c8-4f69-ad88-b6e6dfecc8f7", CategoryId = 3, Description = "Product_2 description", Name = "Product_2", Price = 12m },
                        new { Id = "487fc88d-493a-4474-8f8b-a46ab2a327ac", CategoryId = 1, Description = "Product_3 description", Name = "Product_3", Price = 13m },
                        new { Id = "1c961b79-e24c-4f4d-8df2-3ae477d8a91c", CategoryId = 2, Description = "Product_4 description", Name = "Product_4", Price = 14m },
                        new { Id = "25ca98cb-2840-4775-89b0-05d30085cdf0", CategoryId = 3, Description = "Product_5 description", Name = "Product_5", Price = 15m },
                        new { Id = "d7f03c10-22c6-49bc-97e8-747e00d67481", CategoryId = 1, Description = "Product_6 description", Name = "Product_6", Price = 16m },
                        new { Id = "421fa313-aa0a-4d0a-abe8-e446828ab4ea", CategoryId = 2, Description = "Product_7 description", Name = "Product_7", Price = 17m },
                        new { Id = "3d0f62d1-b0f2-48cb-a2e9-abcb7ab88ffa", CategoryId = 3, Description = "Product_8 description", Name = "Product_8", Price = 18m },
                        new { Id = "2b7aeb11-607a-4a04-a0f5-7976f3acdbd0", CategoryId = 1, Description = "Product_9 description", Name = "Product_9", Price = 19m },
                        new { Id = "9cba2248-585b-49ff-9831-56f3551981ae", CategoryId = 2, Description = "Product_10 description", Name = "Product_10", Price = 20m },
                        new { Id = "62e23f61-6060-4a27-b551-541c16691d3b", CategoryId = 3, Description = "Product_11 description", Name = "Product_11", Price = 21m },
                        new { Id = "edbcd74f-59ed-485f-ba0a-fb8980bf1449", CategoryId = 1, Description = "Product_12 description", Name = "Product_12", Price = 22m },
                        new { Id = "3273a01f-2721-4239-bb3c-bf53a24463a2", CategoryId = 2, Description = "Product_13 description", Name = "Product_13", Price = 23m },
                        new { Id = "b8e1a703-6d90-461c-b032-cc3f9aadd63c", CategoryId = 3, Description = "Product_14 description", Name = "Product_14", Price = 24m },
                        new { Id = "79bbc44b-ff40-4c18-bc51-ba0f29568290", CategoryId = 1, Description = "Product_15 description", Name = "Product_15", Price = 25m },
                        new { Id = "cab1b111-5a4f-41de-9959-7b562a812644", CategoryId = 2, Description = "Product_16 description", Name = "Product_16", Price = 26m },
                        new { Id = "76893f88-b351-4c49-9c51-b0315c9ac3a2", CategoryId = 3, Description = "Product_17 description", Name = "Product_17", Price = 27m },
                        new { Id = "68acc572-1daa-4a12-acdf-a5604a2f87aa", CategoryId = 1, Description = "Product_18 description", Name = "Product_18", Price = 28m },
                        new { Id = "33685d23-4a9d-4725-8bea-4ca462d375cc", CategoryId = 2, Description = "Product_19 description", Name = "Product_19", Price = 29m },
                        new { Id = "5dcd3cc7-0caa-478a-9881-547950040bb4", CategoryId = 3, Description = "Product_20 description", Name = "Product_20", Price = 30m },
                        new { Id = "ebbfe96a-dafd-46a0-bc0f-10dadf53f43c", CategoryId = 1, Description = "Product_21 description", Name = "Product_21", Price = 31m },
                        new { Id = "9e5a35c1-bee3-42c5-a1cc-e13aa933be39", CategoryId = 2, Description = "Product_22 description", Name = "Product_22", Price = 32m },
                        new { Id = "fc81983d-94ab-418a-8905-59bec84b5d26", CategoryId = 3, Description = "Product_23 description", Name = "Product_23", Price = 33m },
                        new { Id = "041e1177-edd2-491b-a6fb-f185b486f3d8", CategoryId = 1, Description = "Product_24 description", Name = "Product_24", Price = 34m },
                        new { Id = "e10768c0-e846-441b-bfab-3ec3b7e8fa3d", CategoryId = 2, Description = "Product_25 description", Name = "Product_25", Price = 35m },
                        new { Id = "efde8f45-208d-4989-b670-473510889c4e", CategoryId = 3, Description = "Product_26 description", Name = "Product_26", Price = 36m },
                        new { Id = "b89bccaa-1e85-4305-9cea-6dd89083a14b", CategoryId = 1, Description = "Product_27 description", Name = "Product_27", Price = 37m },
                        new { Id = "97247a0e-f050-47f4-8e44-6815249fb3bc", CategoryId = 2, Description = "Product_28 description", Name = "Product_28", Price = 38m },
                        new { Id = "a5a2e160-169d-4b5f-9c91-c696f5509d96", CategoryId = 3, Description = "Product_29 description", Name = "Product_29", Price = 39m },
                        new { Id = "8f8a52d7-3015-41a1-83d1-5c7af399eb23", CategoryId = 1, Description = "Product_30 description", Name = "Product_30", Price = 40m },
                        new { Id = "68e8af39-ac5a-40d3-a971-211649c8ff67", CategoryId = 2, Description = "Product_31 description", Name = "Product_31", Price = 41m },
                        new { Id = "ae5700a6-8da8-4ede-971e-16b955b7df9d", CategoryId = 3, Description = "Product_32 description", Name = "Product_32", Price = 42m },
                        new { Id = "7d87b64a-feb7-4514-8d28-4864caaa8037", CategoryId = 1, Description = "Product_33 description", Name = "Product_33", Price = 43m },
                        new { Id = "4ea7f9ec-ebdb-4590-8aef-f102686cbe09", CategoryId = 2, Description = "Product_34 description", Name = "Product_34", Price = 44m },
                        new { Id = "6c8cd85d-27a1-423a-b3e3-aa01d01d3165", CategoryId = 3, Description = "Product_35 description", Name = "Product_35", Price = 45m },
                        new { Id = "5c76d09f-9d24-429c-8b10-373b26ab5a94", CategoryId = 1, Description = "Product_36 description", Name = "Product_36", Price = 46m },
                        new { Id = "517f2fef-eba8-4eb8-ab85-2969240e0954", CategoryId = 2, Description = "Product_37 description", Name = "Product_37", Price = 47m },
                        new { Id = "5bb41cef-8165-41ee-ac06-f052602c82cb", CategoryId = 3, Description = "Product_38 description", Name = "Product_38", Price = 48m },
                        new { Id = "21401d41-48a2-4a96-9893-1e452c020a27", CategoryId = 1, Description = "Product_39 description", Name = "Product_39", Price = 49m },
                        new { Id = "47e5ddbe-570f-4cae-94f6-29617f158bc6", CategoryId = 2, Description = "Product_40 description", Name = "Product_40", Price = 50m },
                        new { Id = "5fa70c1a-ddd2-43fa-9120-de02f236de73", CategoryId = 3, Description = "Product_41 description", Name = "Product_41", Price = 51m },
                        new { Id = "387cfdb5-0df5-4e0f-bdc6-ecfc0087b15e", CategoryId = 1, Description = "Product_42 description", Name = "Product_42", Price = 52m },
                        new { Id = "caf7f43d-4993-4202-8e8d-10f425d070c0", CategoryId = 2, Description = "Product_43 description", Name = "Product_43", Price = 53m },
                        new { Id = "944d4be4-0de4-44ce-b76c-3b47866ca6b5", CategoryId = 3, Description = "Product_44 description", Name = "Product_44", Price = 54m },
                        new { Id = "41bb3c04-0dc9-4d4c-8b0e-5cf79901ba4e", CategoryId = 1, Description = "Product_45 description", Name = "Product_45", Price = 55m },
                        new { Id = "2d631e2a-ac9c-4690-9fd9-f4159c2f8ccc", CategoryId = 2, Description = "Product_46 description", Name = "Product_46", Price = 56m },
                        new { Id = "a0ea037d-682c-451f-89c9-b857f28f66f1", CategoryId = 3, Description = "Product_47 description", Name = "Product_47", Price = 57m },
                        new { Id = "4e8abeb0-3565-426c-ba82-6aab6030b296", CategoryId = 1, Description = "Product_48 description", Name = "Product_48", Price = 58m },
                        new { Id = "2c970f88-4a8d-4558-910f-142d1a71f958", CategoryId = 2, Description = "Product_49 description", Name = "Product_49", Price = 59m },
                        new { Id = "e554d6fe-5b1c-4017-9200-c7fec18a91ed", CategoryId = 3, Description = "Product_50 description", Name = "Product_50", Price = 60m },
                        new { Id = "f61cb14d-df91-4773-804a-0a85a1cb22a0", CategoryId = 1, Description = "Product_51 description", Name = "Product_51", Price = 61m },
                        new { Id = "bd8a43dc-6467-4925-ba95-625490eabb58", CategoryId = 2, Description = "Product_52 description", Name = "Product_52", Price = 62m },
                        new { Id = "fdfc2f59-7fea-45f8-ae4d-57d6ddf6018a", CategoryId = 3, Description = "Product_53 description", Name = "Product_53", Price = 63m },
                        new { Id = "59722b1a-9c46-4a0a-985a-042fdf22b3d8", CategoryId = 1, Description = "Product_54 description", Name = "Product_54", Price = 64m },
                        new { Id = "c67f7fda-6e92-4409-9fbe-158e6140b108", CategoryId = 2, Description = "Product_55 description", Name = "Product_55", Price = 65m },
                        new { Id = "2c12319d-d6c8-44a7-be6d-b2c09b44bb11", CategoryId = 3, Description = "Product_56 description", Name = "Product_56", Price = 66m },
                        new { Id = "5a26893c-d484-4395-b080-e01ebd21b261", CategoryId = 1, Description = "Product_57 description", Name = "Product_57", Price = 67m },
                        new { Id = "e5e2afc4-a3e3-4726-a927-3e440426d7d6", CategoryId = 2, Description = "Product_58 description", Name = "Product_58", Price = 68m },
                        new { Id = "8a5801e2-8f7e-447b-bf4c-9ab9f3c3b983", CategoryId = 3, Description = "Product_59 description", Name = "Product_59", Price = 69m },
                        new { Id = "edefbac8-4f55-48c7-a62d-2e91d821a4ec", CategoryId = 1, Description = "Product_60 description", Name = "Product_60", Price = 70m },
                        new { Id = "c0fb40b8-d5cb-4a35-abfc-e7876531c6ff", CategoryId = 2, Description = "Product_61 description", Name = "Product_61", Price = 71m },
                        new { Id = "47a5c459-ba52-422f-abb5-830317ff44dc", CategoryId = 3, Description = "Product_62 description", Name = "Product_62", Price = 72m },
                        new { Id = "f880d733-15eb-4b82-950f-39f98c83de51", CategoryId = 1, Description = "Product_63 description", Name = "Product_63", Price = 73m },
                        new { Id = "85e84e24-295f-43f1-bd14-4754b57c06c6", CategoryId = 2, Description = "Product_64 description", Name = "Product_64", Price = 74m },
                        new { Id = "96acae99-789e-4168-b9ea-08528a524d08", CategoryId = 3, Description = "Product_65 description", Name = "Product_65", Price = 75m },
                        new { Id = "0289bd9a-3608-4822-bccf-216c75cebe94", CategoryId = 1, Description = "Product_66 description", Name = "Product_66", Price = 76m },
                        new { Id = "8abcc151-cfbb-4d4e-81fa-53860b5e9361", CategoryId = 2, Description = "Product_67 description", Name = "Product_67", Price = 77m },
                        new { Id = "4761b0c2-154c-4f2b-92af-08b8789b1066", CategoryId = 3, Description = "Product_68 description", Name = "Product_68", Price = 78m },
                        new { Id = "7ebe858c-c635-4fad-b428-ebe3c6fe0490", CategoryId = 1, Description = "Product_69 description", Name = "Product_69", Price = 79m },
                        new { Id = "26a29307-26cf-4080-a505-7373e2bc1f78", CategoryId = 2, Description = "Product_70 description", Name = "Product_70", Price = 80m },
                        new { Id = "df6c79fe-126a-4150-aedf-ae45cde4b905", CategoryId = 3, Description = "Product_71 description", Name = "Product_71", Price = 81m },
                        new { Id = "fbb5c17b-6c0f-47e2-a71b-7e334afa05b3", CategoryId = 1, Description = "Product_72 description", Name = "Product_72", Price = 82m },
                        new { Id = "bec30a15-4bb2-4ebd-ae64-06a697fba0af", CategoryId = 2, Description = "Product_73 description", Name = "Product_73", Price = 83m },
                        new { Id = "36213b31-b369-4f3f-a9bb-9b6cc3e3f502", CategoryId = 3, Description = "Product_74 description", Name = "Product_74", Price = 84m },
                        new { Id = "766053b4-9a0e-4113-b335-a04dbfc8f8b6", CategoryId = 1, Description = "Product_75 description", Name = "Product_75", Price = 85m },
                        new { Id = "2913507f-8435-4e65-bdfc-5ef03ca804c5", CategoryId = 2, Description = "Product_76 description", Name = "Product_76", Price = 86m },
                        new { Id = "efb4b60c-4386-4f6c-9f7a-c03338955b9c", CategoryId = 3, Description = "Product_77 description", Name = "Product_77", Price = 87m },
                        new { Id = "30c688eb-6627-4a90-82ad-a5f1dc894eae", CategoryId = 1, Description = "Product_78 description", Name = "Product_78", Price = 88m },
                        new { Id = "9001c5c9-4dc5-4506-a28e-715af70c56d9", CategoryId = 2, Description = "Product_79 description", Name = "Product_79", Price = 89m },
                        new { Id = "d9d3a37d-9f90-4a79-bdca-16f5c5f05eb5", CategoryId = 3, Description = "Product_80 description", Name = "Product_80", Price = 90m },
                        new { Id = "6ff4e8e2-3519-4a9a-ba83-df59a03a7d36", CategoryId = 1, Description = "Product_81 description", Name = "Product_81", Price = 91m },
                        new { Id = "c7b36dd1-5870-4597-a292-93059ed89b6b", CategoryId = 2, Description = "Product_82 description", Name = "Product_82", Price = 92m },
                        new { Id = "ebfaa514-f524-4c0b-8cf7-a99324e30e25", CategoryId = 3, Description = "Product_83 description", Name = "Product_83", Price = 93m },
                        new { Id = "1e18372c-5655-4574-a47c-aaabe7c8c608", CategoryId = 1, Description = "Product_84 description", Name = "Product_84", Price = 94m },
                        new { Id = "a0ca9fa9-ac50-4096-b1f6-9d0bf309ddee", CategoryId = 2, Description = "Product_85 description", Name = "Product_85", Price = 95m },
                        new { Id = "2cca49dd-69f7-4bdb-a9fd-a38766a57f28", CategoryId = 3, Description = "Product_86 description", Name = "Product_86", Price = 96m },
                        new { Id = "24ae8976-92e8-4d28-81e2-49555cf9143a", CategoryId = 1, Description = "Product_87 description", Name = "Product_87", Price = 97m },
                        new { Id = "7fc55ac8-54ce-4493-9d36-ca9b7bde4c17", CategoryId = 2, Description = "Product_88 description", Name = "Product_88", Price = 98m },
                        new { Id = "3d3769e9-d156-4306-b376-1d4a36862013", CategoryId = 3, Description = "Product_89 description", Name = "Product_89", Price = 99m },
                        new { Id = "7f2b1f97-6b67-458d-9550-0f8e3cdb7a43", CategoryId = 1, Description = "Product_90 description", Name = "Product_90", Price = 100m },
                        new { Id = "106b0e9a-da8b-4430-b8e4-399c81780e9c", CategoryId = 2, Description = "Product_91 description", Name = "Product_91", Price = 101m },
                        new { Id = "2fe54c87-f1e9-4a5d-843e-f5deae83bd6e", CategoryId = 3, Description = "Product_92 description", Name = "Product_92", Price = 102m },
                        new { Id = "4327e635-ec8f-482f-aa8c-2b1e205c01f6", CategoryId = 1, Description = "Product_93 description", Name = "Product_93", Price = 103m },
                        new { Id = "c4ee16ce-5443-4a1b-afba-a3587caa4474", CategoryId = 2, Description = "Product_94 description", Name = "Product_94", Price = 104m },
                        new { Id = "89d5e9e8-7d78-45c6-9cf5-d648015b5091", CategoryId = 3, Description = "Product_95 description", Name = "Product_95", Price = 105m },
                        new { Id = "adf04b9d-e466-4de8-abc5-b1224e430dc3", CategoryId = 1, Description = "Product_96 description", Name = "Product_96", Price = 106m },
                        new { Id = "e6f0b3e2-b830-42ba-b3f6-d697a6e842e8", CategoryId = 2, Description = "Product_97 description", Name = "Product_97", Price = 107m },
                        new { Id = "2ece79d9-3778-4248-b298-cbebb24c004c", CategoryId = 3, Description = "Product_98 description", Name = "Product_98", Price = 108m },
                        new { Id = "6ed5d252-6d57-4c9f-80ed-cc0c4f9a203c", CategoryId = 1, Description = "Product_99 description", Name = "Product_99", Price = 109m }
                    );
                });
#pragma warning restore 612, 618
        }
    }
}
