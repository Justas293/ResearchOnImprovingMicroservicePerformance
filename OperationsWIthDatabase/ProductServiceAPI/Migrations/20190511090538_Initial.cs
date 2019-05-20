using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductServiceAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Electronic Items", "Electronics" },
                    { 2, "Dresses", "Clothes" },
                    { 3, "Grocery Items", "Grocery" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { "df6c79fe-126a-4150-aedf-ae45cde4b905", 3, "Product_71 description", "Product_71", 81m },
                    { "26a29307-26cf-4080-a505-7373e2bc1f78", 2, "Product_70 description", "Product_70", 80m },
                    { "7ebe858c-c635-4fad-b428-ebe3c6fe0490", 1, "Product_69 description", "Product_69", 79m },
                    { "4761b0c2-154c-4f2b-92af-08b8789b1066", 3, "Product_68 description", "Product_68", 78m },
                    { "8abcc151-cfbb-4d4e-81fa-53860b5e9361", 2, "Product_67 description", "Product_67", 77m },
                    { "0289bd9a-3608-4822-bccf-216c75cebe94", 1, "Product_66 description", "Product_66", 76m },
                    { "96acae99-789e-4168-b9ea-08528a524d08", 3, "Product_65 description", "Product_65", 75m },
                    { "85e84e24-295f-43f1-bd14-4754b57c06c6", 2, "Product_64 description", "Product_64", 74m },
                    { "f880d733-15eb-4b82-950f-39f98c83de51", 1, "Product_63 description", "Product_63", 73m },
                    { "47a5c459-ba52-422f-abb5-830317ff44dc", 3, "Product_62 description", "Product_62", 72m },
                    { "edefbac8-4f55-48c7-a62d-2e91d821a4ec", 1, "Product_60 description", "Product_60", 70m },
                    { "fbb5c17b-6c0f-47e2-a71b-7e334afa05b3", 1, "Product_72 description", "Product_72", 82m },
                    { "8a5801e2-8f7e-447b-bf4c-9ab9f3c3b983", 3, "Product_59 description", "Product_59", 69m },
                    { "e5e2afc4-a3e3-4726-a927-3e440426d7d6", 2, "Product_58 description", "Product_58", 68m },
                    { "5a26893c-d484-4395-b080-e01ebd21b261", 1, "Product_57 description", "Product_57", 67m },
                    { "2c12319d-d6c8-44a7-be6d-b2c09b44bb11", 3, "Product_56 description", "Product_56", 66m },
                    { "c67f7fda-6e92-4409-9fbe-158e6140b108", 2, "Product_55 description", "Product_55", 65m },
                    { "59722b1a-9c46-4a0a-985a-042fdf22b3d8", 1, "Product_54 description", "Product_54", 64m },
                    { "fdfc2f59-7fea-45f8-ae4d-57d6ddf6018a", 3, "Product_53 description", "Product_53", 63m },
                    { "bd8a43dc-6467-4925-ba95-625490eabb58", 2, "Product_52 description", "Product_52", 62m },
                    { "f61cb14d-df91-4773-804a-0a85a1cb22a0", 1, "Product_51 description", "Product_51", 61m },
                    { "c0fb40b8-d5cb-4a35-abfc-e7876531c6ff", 2, "Product_61 description", "Product_61", 71m },
                    { "bec30a15-4bb2-4ebd-ae64-06a697fba0af", 2, "Product_73 description", "Product_73", 83m },
                    { "766053b4-9a0e-4113-b335-a04dbfc8f8b6", 1, "Product_75 description", "Product_75", 85m },
                    { "e554d6fe-5b1c-4017-9200-c7fec18a91ed", 3, "Product_50 description", "Product_50", 60m },
                    { "e6f0b3e2-b830-42ba-b3f6-d697a6e842e8", 2, "Product_97 description", "Product_97", 107m },
                    { "adf04b9d-e466-4de8-abc5-b1224e430dc3", 1, "Product_96 description", "Product_96", 106m },
                    { "89d5e9e8-7d78-45c6-9cf5-d648015b5091", 3, "Product_95 description", "Product_95", 105m },
                    { "c4ee16ce-5443-4a1b-afba-a3587caa4474", 2, "Product_94 description", "Product_94", 104m },
                    { "4327e635-ec8f-482f-aa8c-2b1e205c01f6", 1, "Product_93 description", "Product_93", 103m },
                    { "2fe54c87-f1e9-4a5d-843e-f5deae83bd6e", 3, "Product_92 description", "Product_92", 102m },
                    { "106b0e9a-da8b-4430-b8e4-399c81780e9c", 2, "Product_91 description", "Product_91", 101m },
                    { "7f2b1f97-6b67-458d-9550-0f8e3cdb7a43", 1, "Product_90 description", "Product_90", 100m },
                    { "3d3769e9-d156-4306-b376-1d4a36862013", 3, "Product_89 description", "Product_89", 99m },
                    { "7fc55ac8-54ce-4493-9d36-ca9b7bde4c17", 2, "Product_88 description", "Product_88", 98m },
                    { "36213b31-b369-4f3f-a9bb-9b6cc3e3f502", 3, "Product_74 description", "Product_74", 84m },
                    { "24ae8976-92e8-4d28-81e2-49555cf9143a", 1, "Product_87 description", "Product_87", 97m },
                    { "a0ca9fa9-ac50-4096-b1f6-9d0bf309ddee", 2, "Product_85 description", "Product_85", 95m },
                    { "1e18372c-5655-4574-a47c-aaabe7c8c608", 1, "Product_84 description", "Product_84", 94m },
                    { "ebfaa514-f524-4c0b-8cf7-a99324e30e25", 3, "Product_83 description", "Product_83", 93m },
                    { "c7b36dd1-5870-4597-a292-93059ed89b6b", 2, "Product_82 description", "Product_82", 92m },
                    { "6ff4e8e2-3519-4a9a-ba83-df59a03a7d36", 1, "Product_81 description", "Product_81", 91m },
                    { "d9d3a37d-9f90-4a79-bdca-16f5c5f05eb5", 3, "Product_80 description", "Product_80", 90m },
                    { "9001c5c9-4dc5-4506-a28e-715af70c56d9", 2, "Product_79 description", "Product_79", 89m },
                    { "30c688eb-6627-4a90-82ad-a5f1dc894eae", 1, "Product_78 description", "Product_78", 88m },
                    { "efb4b60c-4386-4f6c-9f7a-c03338955b9c", 3, "Product_77 description", "Product_77", 87m },
                    { "2913507f-8435-4e65-bdfc-5ef03ca804c5", 2, "Product_76 description", "Product_76", 86m },
                    { "2cca49dd-69f7-4bdb-a9fd-a38766a57f28", 3, "Product_86 description", "Product_86", 96m },
                    { "2c970f88-4a8d-4558-910f-142d1a71f958", 2, "Product_49 description", "Product_49", 59m },
                    { "4e8abeb0-3565-426c-ba82-6aab6030b296", 1, "Product_48 description", "Product_48", 58m },
                    { "a0ea037d-682c-451f-89c9-b857f28f66f1", 3, "Product_47 description", "Product_47", 57m },
                    { "5dcd3cc7-0caa-478a-9881-547950040bb4", 3, "Product_20 description", "Product_20", 30m },
                    { "33685d23-4a9d-4725-8bea-4ca462d375cc", 2, "Product_19 description", "Product_19", 29m },
                    { "68acc572-1daa-4a12-acdf-a5604a2f87aa", 1, "Product_18 description", "Product_18", 28m },
                    { "76893f88-b351-4c49-9c51-b0315c9ac3a2", 3, "Product_17 description", "Product_17", 27m },
                    { "cab1b111-5a4f-41de-9959-7b562a812644", 2, "Product_16 description", "Product_16", 26m },
                    { "79bbc44b-ff40-4c18-bc51-ba0f29568290", 1, "Product_15 description", "Product_15", 25m },
                    { "b8e1a703-6d90-461c-b032-cc3f9aadd63c", 3, "Product_14 description", "Product_14", 24m },
                    { "3273a01f-2721-4239-bb3c-bf53a24463a2", 2, "Product_13 description", "Product_13", 23m },
                    { "edbcd74f-59ed-485f-ba0a-fb8980bf1449", 1, "Product_12 description", "Product_12", 22m },
                    { "62e23f61-6060-4a27-b551-541c16691d3b", 3, "Product_11 description", "Product_11", 21m },
                    { "9cba2248-585b-49ff-9831-56f3551981ae", 2, "Product_10 description", "Product_10", 20m },
                    { "2b7aeb11-607a-4a04-a0f5-7976f3acdbd0", 1, "Product_9 description", "Product_9", 19m },
                    { "3d0f62d1-b0f2-48cb-a2e9-abcb7ab88ffa", 3, "Product_8 description", "Product_8", 18m },
                    { "421fa313-aa0a-4d0a-abe8-e446828ab4ea", 2, "Product_7 description", "Product_7", 17m },
                    { "d7f03c10-22c6-49bc-97e8-747e00d67481", 1, "Product_6 description", "Product_6", 16m },
                    { "25ca98cb-2840-4775-89b0-05d30085cdf0", 3, "Product_5 description", "Product_5", 15m },
                    { "1c961b79-e24c-4f4d-8df2-3ae477d8a91c", 2, "Product_4 description", "Product_4", 14m },
                    { "487fc88d-493a-4474-8f8b-a46ab2a327ac", 1, "Product_3 description", "Product_3", 13m },
                    { "daa1ddf2-f1c8-4f69-ad88-b6e6dfecc8f7", 3, "Product_2 description", "Product_2", 12m },
                    { "2e4cdeb7-dbd8-489e-8bf5-c260d2c0e0bb", 2, "Product_1 description", "Product_1", 11m },
                    { "69a6d3a9-8d1b-428c-b6bc-50dd483522a7", 1, "Product_0 description", "Product_0", 10m },
                    { "ebbfe96a-dafd-46a0-bc0f-10dadf53f43c", 1, "Product_21 description", "Product_21", 31m },
                    { "9e5a35c1-bee3-42c5-a1cc-e13aa933be39", 2, "Product_22 description", "Product_22", 32m },
                    { "fc81983d-94ab-418a-8905-59bec84b5d26", 3, "Product_23 description", "Product_23", 33m },
                    { "041e1177-edd2-491b-a6fb-f185b486f3d8", 1, "Product_24 description", "Product_24", 34m },
                    { "2d631e2a-ac9c-4690-9fd9-f4159c2f8ccc", 2, "Product_46 description", "Product_46", 56m },
                    { "41bb3c04-0dc9-4d4c-8b0e-5cf79901ba4e", 1, "Product_45 description", "Product_45", 55m },
                    { "944d4be4-0de4-44ce-b76c-3b47866ca6b5", 3, "Product_44 description", "Product_44", 54m },
                    { "caf7f43d-4993-4202-8e8d-10f425d070c0", 2, "Product_43 description", "Product_43", 53m },
                    { "387cfdb5-0df5-4e0f-bdc6-ecfc0087b15e", 1, "Product_42 description", "Product_42", 52m },
                    { "5fa70c1a-ddd2-43fa-9120-de02f236de73", 3, "Product_41 description", "Product_41", 51m },
                    { "47e5ddbe-570f-4cae-94f6-29617f158bc6", 2, "Product_40 description", "Product_40", 50m },
                    { "21401d41-48a2-4a96-9893-1e452c020a27", 1, "Product_39 description", "Product_39", 49m },
                    { "5bb41cef-8165-41ee-ac06-f052602c82cb", 3, "Product_38 description", "Product_38", 48m },
                    { "517f2fef-eba8-4eb8-ab85-2969240e0954", 2, "Product_37 description", "Product_37", 47m },
                    { "2ece79d9-3778-4248-b298-cbebb24c004c", 3, "Product_98 description", "Product_98", 108m },
                    { "5c76d09f-9d24-429c-8b10-373b26ab5a94", 1, "Product_36 description", "Product_36", 46m },
                    { "4ea7f9ec-ebdb-4590-8aef-f102686cbe09", 2, "Product_34 description", "Product_34", 44m },
                    { "7d87b64a-feb7-4514-8d28-4864caaa8037", 1, "Product_33 description", "Product_33", 43m },
                    { "ae5700a6-8da8-4ede-971e-16b955b7df9d", 3, "Product_32 description", "Product_32", 42m },
                    { "68e8af39-ac5a-40d3-a971-211649c8ff67", 2, "Product_31 description", "Product_31", 41m },
                    { "8f8a52d7-3015-41a1-83d1-5c7af399eb23", 1, "Product_30 description", "Product_30", 40m },
                    { "a5a2e160-169d-4b5f-9c91-c696f5509d96", 3, "Product_29 description", "Product_29", 39m },
                    { "97247a0e-f050-47f4-8e44-6815249fb3bc", 2, "Product_28 description", "Product_28", 38m },
                    { "b89bccaa-1e85-4305-9cea-6dd89083a14b", 1, "Product_27 description", "Product_27", 37m },
                    { "efde8f45-208d-4989-b670-473510889c4e", 3, "Product_26 description", "Product_26", 36m },
                    { "e10768c0-e846-441b-bfab-3ec3b7e8fa3d", 2, "Product_25 description", "Product_25", 35m },
                    { "6c8cd85d-27a1-423a-b3e3-aa01d01d3165", 3, "Product_35 description", "Product_35", 45m },
                    { "6ed5d252-6d57-4c9f-80ed-cc0c4f9a203c", 1, "Product_99 description", "Product_99", 109m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
