namespace Final_assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateData1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IdentityUsers", "FirstName", c => c.String());
            AddColumn("dbo.IdentityUsers", "LastName", c => c.String());
            AddColumn("dbo.IdentityUsers", "DOB", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IdentityUsers", "DOB");
            DropColumn("dbo.IdentityUsers", "LastName");
            DropColumn("dbo.IdentityUsers", "FirstName");
        }
    }
}
