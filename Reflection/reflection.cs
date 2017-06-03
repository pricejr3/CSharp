using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SearchReflection {
	public abstract class Media {
		public String title;
		public Media(String t) {
			title = t;
		}
		public bool Search(String target) {
			return title.IndexOf(target) >= 0;
		}
		public bool MegaSearch(String target) { // Fill this in
			
			bool returnVal = false;


			Type type = this.GetType();


			foreach (FieldInfo info in type.GetFields()) {
				if (info.MemberType == MemberTypes.Field) {


				Object entry = info.GetValue(this);
				String text = (string) Convert.ChangeType(entry, typeof ( String ) );
          			string[] tokens = text.Split(' ');
           		

           			foreach (string s in tokens)
          			{

					if(returnVal == false){
					if(s.Equals(target)){
						returnVal = true;
					}
				   }

          			}
					
			}
		}

			return returnVal;
	}
}
	public class Movie : Media {
		public String director;
		public int runningTime;
		public String star;
		public Movie(String t, String d, int time, String s) : base(t) {
			director = d;
			runningTime = time;
			star = s;
		}
		new public bool Search(String target) 	{
			return director.IndexOf(target) >= 0 || 
					star.IndexOf(target) >= 0 || base.Search(target);
		}


	}
	public class Song : Media {
		public String genre;
		public String artist;
		public Song(String t, String a, String g) : base(t) {
			artist = a;
			genre = g;
		}
		new public bool Search(String target) {
			return genre.IndexOf(target) >= 0 ||
					artist.IndexOf(target) >= 0 || base.Search(target);
		}
	}
	class Program {
		static void Main(string[] args) {
			Song s1 = new Song("All you need is love", "Beatles", "Classic rock");
			Movie v1 = new Movie("Caddyshack", "Harold Ramis", 120, "Bill love Murray");


			Console.WriteLine(s1.Search("love"));
			Console.WriteLine(s1.Search("rock"));
			Console.WriteLine(s1.Search("Stones"));
			Console.WriteLine(v1.Search("Murray"));
			Console.WriteLine(v1.Search("Chase"));
			Console.WriteLine(v1.Search("Caddyshack"));

			Console.WriteLine(s1.MegaSearch("Classic"));
			Console.WriteLine(v1.MegaSearch("love"));


		

		}
	}

}