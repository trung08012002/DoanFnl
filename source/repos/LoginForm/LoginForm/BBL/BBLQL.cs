using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoginForm.DTO;
namespace LoginForm.BBL
{
    internal class BBLQL
    {
        private static BBLQL _Instance;
        public static BBLQL Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new BBLQL();
                return _Instance;
            }
            private set { }

        }
        public quizholeitem quizhole(string chuoi,int sohole)
        {
            quizholeitem temp = new quizholeitem();
            String[] chuoisplit=chuoi.Split(' ');
            List<String> hole=new List<String>(sohole);
            List<int> randomlist = new List<int>();
            Random rand = new Random();
            for (int i=0;i<sohole;i++)
            {
                
                int random = rand.Next(0, chuoisplit.Length);
                while(randomlist.Contains(random))
                {
                    random = rand.Next(0, chuoisplit.Length);
                }
                randomlist.Add(random);
                hole.Add(chuoisplit[random]);
                
            }
            temp= new quizholeitem{
                chuoi = chuoisplit,
                listhole=hole,
                indexhole=randomlist,
            };
            return temp;
        }

       public void removeAccount(string id)
        {
            QLENG db = new QLENG();
            accountUser accountUser = db.AccountUsers.Find(id);
            db.AccountUsers.Remove(accountUser);
            db.SaveChanges();
        }
        public inforuser getinforbyEmail(string email)
        {
            QLENG db = new QLENG();
            inforuser temp= db.Inforusers.Where(e=>e.email == email).FirstOrDefault();
            return temp;
        }
        public inforuserView[] sortByField(string text)
        {
            QLENG db = new QLENG();
            Dictionary<string,Comparison<inforuserView>> sortAlgorithms;
            sortAlgorithms = new Dictionary<string, Comparison<inforuserView>>();
            sortAlgorithms.Add("sortFirstName", sortFirstName);
            sortAlgorithms.Add("sortLastName",sortLastName);
            sortAlgorithms.Add("sortGender", sortGender);
            sortAlgorithms.Add("sortEmail", sortEmail);
            sortAlgorithms.Add("sortSinhNhat", sortSinhNhat);
            sortAlgorithms.Add("sortRoleUser", sortRoleUser);
            
            var userSort = sortAlgorithms[GetUserSortAlgorithmKey(text)];

           
            inforuserView[] temp = BBLQL.Instance.GetInforuserView().ToArray();
            for (int i = 0; i < temp.Length; i++)
            {
                for (int j = 0; j < temp.Length - 1; j++)
                {
                    if (userSort(temp[i], temp[j]) < 0)
                    {
                        inforuserView a = temp[i];
                        temp[i] = temp[j];
                        temp[j] = a;
                    }
                }
            }
            return temp;
        }
        
        public videotrochoi gettrochoivideobyid(String id)
        {
            QLENG db =new QLENG();
            return db.Videotrochois.Find(id);
        }

        private string GetUserSortAlgorithmKey(string txt)
        {
            switch (txt)
            {
                case "firstName":
                    txt = "sortFirstName";
                    break;
                case "lastName":
                    txt = "sortLastName";
                    break;
                case "gender":
                    txt = "sortGender";
                    break;
                case "email":
                    txt = "sortEmail";
                    break;
                case "sinhnhat":
                    txt = "sortSinhNhat";
                    break;
                case "roleUser":
                    txt = "sortRoleUser";
                    break;                
                default:
                    break;
            }
            return txt;

        }
        private static int sortFirstName(inforuserView x, inforuserView y)
        {

            return x.firstName.CompareTo(y.firstName);
        }
        private static int sortLastName(inforuserView x, inforuserView y)
        {
            return x.lastName.CompareTo(y.lastName);
        }
        private static int sortGender(inforuserView x, inforuserView y)
        {
            return x.gender.CompareTo(y.gender);
        }
        private static int sortEmail(inforuserView x, inforuserView y)
        {
            return x.email.CompareTo(y.email);
        }
        private static int sortSinhNhat(inforuserView x, inforuserView y)
        {
            return x.sinhnhat.CompareTo(y.sinhnhat);
        }
        private static int sortRoleUser(inforuserView x, inforuserView y)
        {
            return x.roleUser.CompareTo(y.roleUser);
        }        
        public List<inforuserView> GetInforuserView()
        {
            List<inforuserView> list = new List<inforuserView>();
            QLENG db = new QLENG();
            foreach(inforuser i in db.Inforusers)
            {
                inforuserView temp = new inforuserView();
                temp.firstName = i.firstName;
                temp.lastName= i.lastName;
                temp.email = i.email;
                temp.gender=i.gender;
                temp.sinhnhat=i.sinhnhat;
                temp.roleUser=i.roleUser;
                list.Add(temp);
            }
            return list;
        }
        public void removeInfor(string id)
        {
            QLENG db = new QLENG();
            inforuser temp=db.Inforusers.Find(id);
            db.Inforusers.Remove(temp);
            db.SaveChanges();
        }

        public inforuser getInfoById(string mstk)
        {
            QLENG db = new QLENG();
            inforuser temp=db.Inforusers.Find(mstk);
            return temp;
        }
        public bool checkInfo(string mstk)
        {
            return BBLQL.Instance.getInfoById(mstk) != null ;
        }
        public void adduser(string id,string taikhoan, string matkhau)
        {
            QLENG db = new QLENG();
            db.AccountUsers.Add(new accountUser { id = id,userName=taikhoan,passwordUser=matkhau });
            db.SaveChanges();
        }
        public bool checkeduser(string taikhoan)
        {
            QLENG db=new QLENG();
            foreach(accountUser i in db.AccountUsers)
            {
                if(i.userName==taikhoan)
                {
                    return true;
                }
            }
            return false;
        }
       
        public bool checkuser(string taikhoan, string matkhau)
        {
            foreach (accountUser i in BBLQL.Instance.listallusers())
            {
                if (i.userName == taikhoan && i.passwordUser == matkhau)
                {
                    return true;
                }
            }
            return false;
        }
        public List<accountUser> listallusers()
        {
            List<accountUser> list = new List<accountUser>();
            QLENG db=new QLENG();
            foreach(accountUser i in db.AccountUsers)
            {
                list.Add(i);
            }
            return list;
        }
        public accountUser getAccountByEmail(String email)
        {
            QLENG db = new QLENG();
            accountUser accountUser=BBLQL.Instance.getinforbyEmail(email).user;
            return accountUser;
        }
        public void addinfor(string idaccount,string iduser,string firstName,string lastName,string gender,string email,DateTime sinhnhat,string roleUser,string urlAvatar)
        {
            QLENG db = new QLENG();
            db.Inforusers.Add(new inforuser
            {
                id=iduser,
                firstName=firstName,
                lastName=lastName,
                gender=gender,
                email=email,
                sinhnhat=sinhnhat,
                roleUser=roleUser,
                urlAvatar=urlAvatar,
                idaccount=idaccount,
            }) ;
            db.SaveChanges();
        }
        public void updateinfor(string iduser, string firstName, string lastName, string gender, string email, DateTime sinhnhat, string roleUser, string urlAvatar)
        {
            QLENG db = new QLENG();
            inforuser temp = db.Inforusers.Find(iduser);
            temp.firstName = firstName;
            temp.lastName = lastName;
            temp.gender = gender;
            temp.email = email;
            temp.sinhnhat = sinhnhat;
            temp.roleUser = roleUser;
            temp.urlAvatar = urlAvatar;           
            db.SaveChanges();
        }

        //game doan hinh
        public Image ConvertByteArrayToImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }
        public doanhinhDTO randomhinh(int index,string[] listHinhAnh,int[] listindex)
        {
            bool check;
            Random rd = new Random();
            do
            {
                
                index = rd.Next(0, listHinhAnh.Length);
                check = true;
                for (int i = 0; i < listindex.Length; i++)
                {
                   
                    if (index == listindex[i])
                    {
                        
                        check = false;
                        break;
                    }
                }
            } while (check == false);
            return new doanhinhDTO
            {
                
                listHinhAnh = listHinhAnh,
                index=index,
                listindex=listindex,
            };
        }

       

        public int lengthcrossword(string DapAn)
        {
            string[] splitwords = DapAn.Split(' ');
            string temp = "";
            foreach (string i in splitwords)
            {
                temp += i;
            }
            char[] words = temp.ToCharArray();
            
            return words.Length;
        }
        public char[] randomanswer(string DapAn)
        {
            string[] splitwords = DapAn.Split(' ');
            string temp = "";
            
            foreach (string i in splitwords)
            {
                temp += i;
            }          
            char[] words = temp.ToCharArray();
           
            char[] random=new char[words.Length*2];
            List<int> randomlist =new List<int>(words.Length*2);
            List<int> listusedwords=new List<int>(words.Length);
            Random rd = new Random();
            int index;
            int indexrandomwords;
            for (int i = 0; i < 2*words.Length; i++)
            {
                
                index = rd.Next(0, words.Length*2);
               
                while (randomlist.Contains(index))
                {
                    index = rd.Next(0, words.Length * 2 );
                }
                randomlist.Add(index);
                if (listusedwords.Count<words.Length)
                {
                    indexrandomwords = rd.Next(0, words.Length);
                    while (listusedwords.Contains(indexrandomwords))
                    {
                        indexrandomwords = rd.Next(0, words.Length);
                    }
                    listusedwords.Add(indexrandomwords);
                    random[index]=words[indexrandomwords];
                }
                 else if(randomlist.Count+1==words.Length)
                {
                    indexrandomwords = rd.Next(0, words.Length);
                    while (listusedwords.Contains(indexrandomwords))
                    {
                        indexrandomwords = rd.Next(0, words.Length);
                    }
                    listusedwords.Add(indexrandomwords);
                    random[index] = words[indexrandomwords];

                }    
                else random[index] =(char)rd.Next(65,91);


            }
           
            
            return random;
        }
        public bool checkdapanBBl(string temp, string[] listDapAn, int index)
        {
           
            //string dapan = (listDapAn[index].Split(' ')).ToString().ToUpper();            
            string temp1 = temp.ToUpper();
           
            if (temp1 == listDapAn[index].ToUpper())
                return true;
            else return false;
        }
        public void deletehinhanh(string msha)
        {
            QLENG db=new QLENG();
            ImageDoanHinh temp=db.ImageDoanHinhs.Find(msha);
            db.ImageDoanHinhs.Remove(temp);
            db.SaveChanges();
        }
        public void addhinhanh(string i,string urlfile,string dapan)
        {
            QLENG db = new QLENG();
            db.ImageDoanHinhs.Add(new ImageDoanHinh { id = i, url = urlfile, content = dapan });
            db.SaveChanges();
        }
        //Tao Khoa Hoc Video
        public void addKhoaHocVideo(string id,string titlekhoahoc,string linkvideo,string author,string titlevideo,string transcript,string url)
        {
            QLENG db = new QLENG();
            DateTime dateTime = DateTime.Now;
            khoahoc Khoahoc= BBLQL.Instance.getkhoahocbyname(titlekhoahoc);
            video Video=BBLQL.Instance.getvideobyname(titlevideo);
            if(Khoahoc!=null && Video != null)
            {
                db.KhoaHocs.Add(new khoahoc { idkhoahoc = Khoahoc.idkhoahoc, tittle = titlekhoahoc, ngaytao = dateTime, nguoitao = author, anh = url });
                db.Videos.Add(new video { idvideo = Video.idvideo, title = titlevideo, ngaytao = dateTime, luotview = 0, url = linkvideo, transcript = transcript });
                db.SaveChanges();
                db.Videotrochois.Add(new videotrochoi { idvideotrochoi = id + id, idkhoahoc = Khoahoc.idkhoahoc, idvideo = Video.idvideo, datetime = dateTime });
                db.SaveChanges();
            }
            else
            {
                db.KhoaHocs.Add(new khoahoc { idkhoahoc = id, tittle = titlekhoahoc, ngaytao = dateTime, nguoitao = author, anh = url });
                db.Videos.Add(new video { idvideo = id, title = titlevideo, ngaytao = dateTime, luotview = 0, url = linkvideo, transcript = transcript });
                db.SaveChanges();
                db.Videotrochois.Add(new videotrochoi { idvideotrochoi = id + id, idkhoahoc = id, idvideo = id, datetime = dateTime });
                db.SaveChanges();
            }
           
        }
        public video getvideobyname(String name)
        {
            QLENG db = new QLENG();
            return db.Videos.Where(vd=>vd.title==name).FirstOrDefault();
        }
        public video getvideobyid(string id)
        {
            QLENG db = new QLENG();
            video Video = db.Videos.Find(id);
            return Video;
        }

        public khoahoc getkhoahocbyid(string id)
        {
            QLENG db = new QLENG();
            khoahoc khoahoc = db.KhoaHocs.Find(id);
            return khoahoc;

        }

        public void adddatetimeintovdtc(String name,DateTime date)
        {
            QLENG db = new QLENG();
            videotrochoi temp = BBLQL.Instance.getvideotrochoibyname(name);
            temp.datetime = date;            
            db.SaveChanges();
        }
        public List<int> getNams()
        {
            QLENG db = new QLENG();
            List<int> temp = new List<int>();
            foreach(videotrochoi i in db.Videotrochois)
            {
                temp.Add(i.datetime.Year);
            }
            return temp;
        }
        public int[] thongkeviewtheonam(int nam)
        {
            QLENG db=new QLENG();
            int[] temp = new int[13];
            for (int i = 1; i < 13; i++)
            {
                temp[i] = 0;
            }
            foreach(videotrochoi i in db.Videotrochois)
            {
               if(i.datetime.Year == nam)
                {
                    if (temp[i.datetime.Month] == 0)
                    {
                        temp[i.datetime.Month] = 1;
                    }
                    else
                    {
                        temp[i.datetime.Month]++;
                    }
                }                        
                   
            }
            return temp;
        }
        public khoahoc getkhoahocbyname(String name)
        {
            QLENG db=new QLENG();
            return db.KhoaHocs.Where(kh=>kh.tittle==name).FirstOrDefault();
        }
        public videotrochoi getvideotrochoibyname(String name)
        {
            QLENG db=new QLENG();
            String id = BBLQL.Instance.getkhoahocbyname(name).idkhoahoc;
            return db.Videotrochois.Where(vd=>vd.idkhoahoc==id).FirstOrDefault();            
        }
        public void deletekhoahoctrochoi(string id)
        {
            QLENG db = new QLENG();
            videotrochoi vdtc=db.Videotrochois.Find(id);
            db.Videotrochois.Remove(vdtc);
            db.SaveChanges();
            khoahoc kh=db.KhoaHocs.Find(id);
            db.KhoaHocs.Remove(kh);
            video vd=db.Videos.Find(id);
            db.Videos.Remove(vd);
            db.SaveChanges();

        }

        //trie
        

        //thong ke

        
        public void addKhoaHocDaHoc(string iduser,string idkhoahoc)
        {
            QLENG db = new QLENG();
            db.Khoadahocs.Add(new khoadahoc { id = iduser + Convert.ToString(DateTime.Now),iduser=iduser,idkhoahoc=idkhoahoc });
            db.SaveChanges();
        }
        public List<khoadahoc> ShowKhoaDaHoc(string query)
        {
            QLENG db = new QLENG();
            List<khoadahoc> list = new List<khoadahoc>();
            foreach(khoadahoc i in db.Khoadahocs)
            {
               
            }
            return list; 
        }
        public List<String> recommendlist(string goiy)
        {
            List<String> list = new List<String>();
            
            return list;
        }
        public List<int> searchparagraph(string paragraph,string par,trietree tree)
        {
            List<int> list = new List<int>();           
            list=tree.search_tree(par);
            return list;
        }
        public string addParagraphGUI(string id)
        {
            QLENG db = new QLENG();
            videotrochoi vdtc=db.Videotrochois.Find(id);
            return vdtc.video.transcript;
         }
        //public List<int> searchstring(string paragrapth)
       // {

        //}
    }
}