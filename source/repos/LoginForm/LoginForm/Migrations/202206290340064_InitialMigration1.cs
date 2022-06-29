namespace LoginForm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.datetimeusers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ngay = c.Int(nullable: false),
                        thang = c.Int(nullable: false),
                        nam = c.Int(nullable: false),
                        videotrochoi_idvideotrochoi = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.videotrochois", t => t.videotrochoi_idvideotrochoi)
                .Index(t => t.videotrochoi_idvideotrochoi);
            
        }
        
        public override void Down()
        {
          
        }
    }
}
