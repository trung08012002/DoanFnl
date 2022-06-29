namespace LoginForm.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.accountUsers",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        userName = c.String(nullable: false),
                        passwordUser = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ImageDoanHinhs",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        url = c.String(nullable: false),
                        content = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.inforusers",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        firstName = c.String(),
                        lastName = c.String(),
                        gender = c.String(),
                        email = c.String(),
                        sinhnhat = c.DateTime(nullable: false),
                        roleUser = c.String(),
                        idaccount = c.String(maxLength: 128),
                        urlAvatar = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.accountUsers", t => t.idaccount)
                .Index(t => t.idaccount);
            
            CreateTable(
                "dbo.khoadahocs",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        idkhoahoc = c.String(maxLength: 128),
                        iduser = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.inforusers", t => t.iduser)
                .ForeignKey("dbo.khoahocs", t => t.idkhoahoc)
                .Index(t => t.idkhoahoc)
                .Index(t => t.iduser);
            
            CreateTable(
                "dbo.khoahocs",
                c => new
                    {
                        idkhoahoc = c.String(nullable: false, maxLength: 128),
                        tittle = c.String(),
                        anh = c.String(),
                        ngaytao = c.DateTime(nullable: false),
                        nguoitao = c.String(),
                    })
                .PrimaryKey(t => t.idkhoahoc);
            
            CreateTable(
                "dbo.videos",
                c => new
                    {
                        idvideo = c.String(nullable: false, maxLength: 128),
                        title = c.String(),
                        transcript = c.String(),
                        url = c.String(),
                        ngaytao = c.DateTime(nullable: false),
                        luotview = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.idvideo);
            
            CreateTable(
                "dbo.videotrochois",
                c => new
                    {
                        idvideotrochoi = c.String(nullable: false, maxLength: 128),
                        idkhoahoc = c.String(maxLength: 128),
                        idvideo = c.String(maxLength: 128),
                        
                    })
                .PrimaryKey(t => t.idvideotrochoi)
                .ForeignKey("dbo.khoahocs", t => t.idkhoahoc)
                .ForeignKey("dbo.videos", t => t.idvideo)
                .Index(t => t.idkhoahoc)
                .Index(t => t.idvideo);
            
        }
        
        public override void Down()
        {
           
           
        }
    }
}
