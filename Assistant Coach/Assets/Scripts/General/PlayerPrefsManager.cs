using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsManager
{
	public static class SavedSessions
	{
		const string SESSION = "session_";
		const string SUBJECT = "subject";
		const string EXISTS = "exists_";
		const string COUNT = "count_";
		const string MAX = "max_";
		const string DATE = "date_";
		const string SUMMARY = "summary_";
		const string INDEX = "index_";
		const string TIME = "time_";
		const string AGE = "age_";

		static int MaxSessionIdex {
			get { return PlayerPrefs.GetInt(SESSION + COUNT + MAX); }
			set { PlayerPrefs.SetInt(SESSION + COUNT + MAX, value); }
		}

		public static void AddSession (SaveObject saveStruct)
		{
			MaxSessionIdex++;
			var sessionIndex = MaxSessionIdex;
			var session_path = SESSION + sessionIndex;

			PlayerPrefs.SetInt (session_path + EXISTS, 1);
			PlayerPrefs.SetInt (session_path + AGE, saveStruct.age);
			PlayerPrefs.SetInt (session_path + TIME, saveStruct.totalTime);
			PlayerPrefs.SetString (session_path + SUMMARY, saveStruct.TopicSummary);
			SetDate(saveStruct.date, sessionIndex);

			var subjectData = saveStruct.subjectDataArray;

			int s_index = 0;
			foreach (var subject in subjectData)
			{
				PlayerPrefs.SetInt(session_path + s_index + SUBJECT, (int)subject.subject);
				PlayerPrefs.SetInt(session_path + s_index + TIME, subject.time);
				PlayerPrefs.SetInt(session_path + s_index + INDEX, subject.dataBaseIndex);

				s_index++;
			}

			PlayerPrefs.SetInt (session_path + SUBJECT + COUNT, s_index);
		}

		public static SaveObject GetSavedSession (int index)
		{
			var session_path = SESSION + index;

			if (PlayerPrefs.GetInt(session_path + EXISTS) != 1) {
				return new SaveObject ();
			}

			var age = PlayerPrefs.GetInt (session_path + AGE);
			var total_time = PlayerPrefs.GetInt (session_path + TIME);
			//		var summary = PlayerPrefs.GetString (session_path + SUMMARY);
			var date = GetDate(index);

			var subjectData = new List<SubjectData> ();

			int total_subjects = PlayerPrefs.GetInt (session_path + SUBJECT + COUNT);

			for (int i = 0; i < total_subjects; i++)
			{
				var i_subject = (Subjects)PlayerPrefs.GetInt(session_path + total_subjects + SUBJECT);
				var i_time = PlayerPrefs.GetInt(session_path + total_subjects + TIME);
				var i_index = PlayerPrefs.GetInt(session_path + total_subjects + INDEX);

				subjectData.Add(new SubjectData (i_index, i_time, i_subject));
			}

			return new SaveObject(subjectData.ToArray(), date, age, total_time);
		}

		public static void RemoveSession (int sessionIndex)
		{
			string session_path = SESSION + sessionIndex;

			if (PlayerPrefs.GetInt(session_path + EXISTS) != 1) {
				return;
			}

			PlayerPrefs.DeleteKey (session_path + AGE);
			PlayerPrefs.DeleteKey (session_path + TIME);
			PlayerPrefs.DeleteKey (session_path + SUMMARY);

			RemoveDate(sessionIndex);

			int total_subjects = PlayerPrefs.GetInt (session_path + SUBJECT + COUNT);

			for (int i = 0; i < total_subjects; i++)
			{
				PlayerPrefs.DeleteKey(session_path + total_subjects + SUBJECT);
				PlayerPrefs.DeleteKey(session_path + total_subjects + TIME);
				PlayerPrefs.DeleteKey(session_path + total_subjects + INDEX);
			}

			PlayerPrefs.DeleteKey (session_path + SUBJECT + COUNT);
			PlayerPrefs.DeleteKey (session_path + EXISTS);
		}

		static void SetDate (Date date, int index)
		{
			PlayerPrefs.SetInt (SESSION + index + DATE + "m", date.Month);
			PlayerPrefs.SetInt (SESSION + index + DATE + "d", date.Day);
			PlayerPrefs.SetInt (SESSION + index + DATE + "y", date.Year);
		}

		static Date GetDate (int index)
		{
			if (PlayerPrefs.GetInt (SESSION + index + EXISTS, -1) != 1) {
				return new Date ();
			}

			int m = PlayerPrefs.GetInt (SESSION + index + DATE + "m");
			int d = PlayerPrefs.GetInt (SESSION + index + DATE + "d");
			int y = PlayerPrefs.GetInt (SESSION + index + DATE + "y");

			return new Date (d, m, y);
		}

		static void RemoveDate (int index)
		{
			PlayerPrefs.DeleteKey (SESSION + index + DATE + "m");
			PlayerPrefs.DeleteKey (SESSION + index + DATE + "d");
			PlayerPrefs.DeleteKey (SESSION + index + DATE + "y");
		}

		public static SessionData[] GetSessionDescriptions ()
		{
			var subjectData = new List<SessionData> ();
			int maxSessionIndex = PlayerPrefs.GetInt (SESSION + COUNT + MAX, 1);

			for (int i = 0; i <= maxSessionIndex; i++)
			{
				if (PlayerPrefs.GetInt (SESSION + i + EXISTS, -1) == 1)
				{
					Date date = GetDate (i);
					string summary = PlayerPrefs.GetString (SESSION + i + SUMMARY);
					subjectData.Add (new SessionData (i, date, summary));
				}
			}

			return subjectData.ToArray ();
		}
	}



	public static class Settings
	{
		const string PREFERENCE = "preference_";
		const string DAY = "day_";
		const string MONTH = "month_";
		const string YEAR = "year_";
		const string TIME = "time_";
		const string AGE = "age_";

		public static int Age {
			get {return PlayerPrefs.GetInt (PREFERENCE + AGE);}
			set {PlayerPrefs.SetInt (PREFERENCE + AGE, value);}
		}

		public static int Day {
			get {return PlayerPrefs.GetInt (PREFERENCE + DAY);}
			set {PlayerPrefs.SetInt (PREFERENCE + DAY, value);}
		}

		public static int Month {
			get {return PlayerPrefs.GetInt (PREFERENCE + MONTH);}
			set {PlayerPrefs.SetInt (PREFERENCE + MONTH, value);}
		}

		public static int Year {
			get {return PlayerPrefs.GetInt (PREFERENCE + YEAR);}
			set {PlayerPrefs.SetInt (PREFERENCE + YEAR, value);}
		}

		public static List<int> Timeline
		{
			get {
				var list = new List<int> ();
				list.Add (PlayerPrefs.GetInt (PREFERENCE + TIME + 0, 20));
				list.Add (PlayerPrefs.GetInt (PREFERENCE + TIME + 1, 20));
				list.Add (PlayerPrefs.GetInt (PREFERENCE + TIME + 2, 20));
				list.Add (PlayerPrefs.GetInt (PREFERENCE + TIME + 3, 20));
				return list;
			}
			set {
				PlayerPrefs.SetInt (PREFERENCE + TIME, 1);
				for (int i = 0; i < value.Count; i++) {
					PlayerPrefs.SetInt (PREFERENCE + TIME + i, value[i]);
				}
			}
		}
	}
}


//	public static void AddSession (SaveObject saveStruct)
//	{
//		int sessionIndex = FindEmptySessionSlot ();
//		var save_path = SESSION + sessionIndex;
//
//
//		int blocks = 0;
//		foreach (var idexTopicPair in saveStruct.subjectDataArray)
//		{
//			var topic_path = save_path + COUNT + blocks;
//
//			PlayerPrefs.SetString (topic_path, idexTopicPair.subject.ToString ());
//			PlayerPrefs.SetInt (topic_path + idexTopicPair.subject, idexTopicPair.dataBaseIndex);
//			PlayerPrefs.SetInt (topic_path + idexTopicPair.subject + INDEX, (int)idexTopicPair.subject);
//
//			blocks++;
//		}
//
//
//		SetDate (saveStruct.date, sessionIndex);
//		//SetTimes (saveStruct.timeList, sessionIndex);
//
//		PlayerPrefs.SetInt (save_path + EXISTS, 1);
//		PlayerPrefs.SetInt (save_path + SUBJECT + COUNT + MAX, blocks);
//		PlayerPrefs.SetInt (save_path + TIME, saveStruct.totalTime);
//		PlayerPrefs.SetInt (save_path + AGE, saveStruct.age);
//		PlayerPrefs.SetString (save_path + SUMMARY, saveStruct.TopicSummary);
//	}
//
//	static int FindEmptySessionSlot ()
//	{
//		int maxSlotsNeededToCheck = PlayerPrefs.GetInt (SESSION + COUNT + MAX, -1) + 1;
//		int index;
//		for (index = 0; index <= maxSlotsNeededToCheck; index++)
//		{
//			if (PlayerPrefs.GetInt (SESSION + index + EXISTS, -1) != 1) {
//				break;
//			}
//		}
//
//		if (index > maxSlotsNeededToCheck - 1)
//		{
//			PlayerPrefs.SetInt (SESSION + COUNT + MAX, index);
//		}
//
//		return index;
//	}
//
//	public static SaveObject GetSavedSession (int index)
//	{
//		int x = index;
//		if (!PlayerPrefs.HasKey (SESSION + COUNT + index))
//		{
//			return new SaveObject ();
//		}
//
//		int count = PlayerPrefs.GetInt (SESSION + index + COUNT + MAX);
//		List<SubjectData> itps = new List<SubjectData> ();
//
//		for (int i = 0; i < count; i++)
//		{
//			string topic = PlayerPrefs.GetString (SESSION + x + COUNT + i);
//			int dataBaseIndex = PlayerPrefs.GetInt (SESSION + x + COUNT + i + topic);
//			int topicIndex = PlayerPrefs.GetInt (SESSION + x + COUNT + i + topic + INDEX);
//
//			itps.Add (new SubjectData (dataBaseIndex, (Subjects)topicIndex));
//		}
//
//		int time = PlayerPrefs.GetInt (SESSION + x + TIME);
//		int age	= PlayerPrefs.GetInt (SESSION + x + AGE, 9);
//		Date date = GetDate (x);
//		List<int> timesList 	= new List<int> (GetTimes (x));
//
//
//		return new SaveObject (itps.ToArray (), date, age, time);
//	}
//
//	public static void RemoveSession (int sessionIndex)
//	{
//		int x = sessionIndex;
//		int c = PlayerPrefs.GetInt (SESSION + x + COUNT + MAX);
//
//		for (int i = 0; i < c; i++)
//		{
//			string topic = PlayerPrefs.GetString (SESSION + x + COUNT + i);
//			PlayerPrefs.DeleteKey (SESSION + x + COUNT + i + topic + INDEX);
//			PlayerPrefs.DeleteKey (SESSION + x + COUNT + i + topic);
//			PlayerPrefs.DeleteKey (SESSION + x + COUNT + i);
//		}
//
//
//		RemoveTimes (x);
//		RemoveDate (x);
//
//		PlayerPrefs.DeleteKey (SESSION + COUNT + x);
//		PlayerPrefs.DeleteKey (SESSION + x + COUNT + MAX);
//		PlayerPrefs.DeleteKey (SESSION + x + TIME);
//		PlayerPrefs.DeleteKey (SESSION + x + AGE);
//		PlayerPrefs.DeleteKey (SESSION + x + SUMMARY);
//	}
//
//	//
//
//	private static void SetTimes (List<int> timesList, int index)
//	{
//		for (int i = 0; i < timesList.Count; i++)
//		{
//			PlayerPrefs.SetInt (SESSION + index + COUNT + i + TIME, timesList[i]);
//		}
//
//		PlayerPrefs.SetInt (SESSION + index + TIME + COUNT, timesList.Count);
//	}
//
//	private static List<int> GetTimes (int index)
//	{
//		List<int> list = new List<int> ();
//		int max = PlayerPrefs.GetInt (SESSION + index + TIME + COUNT);
//
//		for (int i = 0; i < max; i++)
//		{
//			list.Add (PlayerPrefs.GetInt (SESSION + index + COUNT + i + TIME));
//		}
//
//		return list;
//	}
//
//	private static void RemoveTimes (int index)
//	{
//		int max = PlayerPrefs.GetInt (SESSION + index + TIME + COUNT);
//
//		for (int i = 0; i < max; i++)
//		{
//			PlayerPrefs.DeleteKey (SESSION + index + COUNT + i + TIME);
//		}
//
//		PlayerPrefs.DeleteKey (SESSION + index + TIME + COUNT);
//	}
