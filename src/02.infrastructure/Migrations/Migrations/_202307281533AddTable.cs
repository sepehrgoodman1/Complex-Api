using FluentMigrator;

namespace ComplexProject.Migrations.Migrations
{
    [Migration(202307281533)]
    public class _202307281533AddTable : Migration
    {


        public override void Up()
        {
            GenrateComplexTable();
            GenerateBlockTable();
            GenerateUnitTable();

        }

       

        public override void Down()
        {
            Delete.Table("Units");
            Delete.Table("Blocks");
            Delete.Table("Complexes");
        }


        private void GenerateUnitTable()
        {
            Create.Table("Units").WithColumn("Id").AsInt32().PrimaryKey().Identity()
                                            .WithColumn("Tenant").AsString().NotNullable()
                                            .WithColumn("BlockId").AsInt32()
                                            .WithColumn("TypeHouse").AsString();
        }

        private void GenerateBlockTable()
        {
            Create.Table("Blocks").WithColumn("Id").AsInt32().PrimaryKey().Identity()
                                  .WithColumn("Name").AsString().NotNullable()
                                  .WithColumn("NumberUnits").AsInt32().NotNullable()
                                  .WithColumn("ComplexId").AsInt32()
                                  .ForeignKey("FK_Blocks_Complexes", "Complexes", "Id");
        }

        private void GenrateComplexTable()
        {
            Create.Table("Complexes").WithColumn("Id").AsInt32().PrimaryKey().Identity()
                                   .WithColumn("Name").AsString().NotNullable()
                                   .WithColumn("NumberUnits").AsInt32().NotNullable();
        }


    }
}
