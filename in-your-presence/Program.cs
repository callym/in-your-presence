using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Timers;
using SharpOSC;

namespace in_your_presence
{
	class BeingWithYou
	{
		enum Messages
		{
			Hello,
			IAmStillHere
		}

		static UDPListener ListeningToYou;
		static UDPSender ReplyingToYou;

		static bool IAmAlone = true;

		static void Main()
		{
			IAmListeningToYou();

			HelloIAmHere();

			Timer AreYouStillWithMe = new Timer(3 * 1000);
			AreYouStillWithMe.Enabled = true;
			AreYouStillWithMe.Elapsed += (o, e) =>
			{
				if (IAmAlone)
				{
					Environment.Exit(0);
				}
				else
				{
					AreYouStillHere();
				}
			};

			Console.ReadLine();
		}

		static void HelloIAmHere()
		{
			ReplyingToYou.Send(new OscMessage("/hello", (int)Messages.Hello));
			IAmAlone = false;
		}

		static void AreYouStillHere()
		{
			ReplyingToYou.Send(new OscMessage("/am/i/alone", (int)Messages.IAmStillHere));
			IAmAlone = true;
		}

		static void GetMessage(OscPacket p)
		{
			Messages message = (Messages)(p as OscMessage).Arguments[0];
			switch (message)
			{
				case Messages.Hello:
					IAmAlone = false;
					break;
				case Messages.IAmStillHere:
					IAmAlone = false;
					break;
				default:
					break;
			}
		}

		private static void IAmListeningToYou()
		{
			int myport = 0;
			int yourport = 0;

			List<int> ports = new List<int>();

			IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
			ipGlobalProperties.GetActiveUdpListeners().ToList().ForEach((t) => ports.Add(t.Port));

			if (!ports.Contains(55555) && !ports.Contains(55556))
			{
				myport = 55555;
				yourport = 55556;
				Console.WriteLine("'in your presence', callym, 2016.\n");
				Console.WriteLine("the comfort of being silent in your company");
			}
			else if (ports.Contains(55555) && !ports.Contains(55556))
			{
				myport = 55556;
				yourport = 55555;
			}
			else
			{
				Environment.Exit(0);
			}

			ListeningToYou = new UDPListener(myport, GetMessage);
			ReplyingToYou = new UDPSender("127.0.0.1", yourport);
		}
	}
}
