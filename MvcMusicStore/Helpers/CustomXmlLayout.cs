using System.Xml;
using log4net.Core;
using log4net.Layout;

namespace MvcMusicStore.Helpers
{
	public class CustomXmlLayout: XmlLayoutBase
	{
		protected override void FormatXml(XmlWriter writer, LoggingEvent loggingEvent)
		{
			writer.WriteStartElement("LogEntry");

			// date
			writer.WriteStartElement("Datetime");
			writer.WriteString(loggingEvent.TimeStamp.ToString("G"));
			writer.WriteEndElement();

			// level
			writer.WriteStartElement("Level");
			writer.WriteString(loggingEvent.Level.DisplayName);
			writer.WriteEndElement();

			// domain
			writer.WriteStartElement("Domain");
			writer.WriteString(loggingEvent.Domain);
			writer.WriteEndElement();

			// message
			writer.WriteStartElement("Message");
			writer.WriteString(loggingEvent.RenderedMessage);
			writer.WriteEndElement();

			writer.WriteEndElement();
		}
	}
}