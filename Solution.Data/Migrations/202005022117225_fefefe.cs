namespace Solution.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fefefe : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Reclamations", "Nom");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reclamations", "Nom", c => c.String());
        }
    }
}
