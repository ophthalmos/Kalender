using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace Kalender
{
    public class Setting
    {
        public string Name; // Der Name der Einstellung
        public string Value; // Der Wert der Einstellung
        public string DefaultValue; // Der Defaultwert für das Lesen
        public bool NotInFile; // Gibt an, ob die Einstellung in der Datei gefunden wurde

        public Setting(string name, string defaultValue) // Konstruktor
        {
            this.Name = name;
            this.DefaultValue = defaultValue;
        }
    }

    internal class Settings : Dictionary<string, Setting> // Auflistung zum Speichern mehrerer Einstellungen
    {
        internal void Add(string settingName, string defaultValue) // Fügt der Auflistung eine neue Einstellung hinzu
        {
            this.Add(settingName, new Setting(settingName, defaultValue));
        }
    }

    internal class Section // Verwaltet eine Einstellungs-Sektion
    {
        public string Name;
        public Settings Settings;
        public Section(string sectionName)
        {
            this.Name = sectionName;
            this.Settings = new Settings();
        }
    }

    internal class Sections : Dictionary<string, Section> // Auflistung zur Speicherung von Sektionen
    {
        internal void Add(string name) { this.Add(name, new Section(name)); }
    }

    internal class Config // Klasse zur Verwaltung von Konfigurationsdaten
    {
        private readonly string fileName; // Happe: readonly zugefügt
        public Sections Sections;
        public Config(string fileName)
        {
            this.fileName = fileName;
            this.Sections = new Sections();
        }

        public bool Load() // Liest die Konfigurationsdaten
        {
            bool returnValue = true;
            XmlDocument xmlDoc = new XmlDocument();
            string appName = Application.ProductName;
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), appName, appName + ".xml");
            //string filePath = Path.Combine(Application.StartupPath, appName + ".xml");

            if (!File.Exists(filePath))
            {
                XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = xmlDoc.DocumentElement;
                xmlDoc.InsertBefore(xmlDeclaration, root);
                xmlDoc.AppendChild(xmlDoc.CreateElement(string.Empty, "config", string.Empty));
                xmlDoc.Save(filePath);
            }

            try { xmlDoc.Load(this.fileName); }
            catch (IOException ex) { MessageBox.Show("Fehler beim Laden der Konfigurationsdatei '" + fileName + "'."  + Environment.NewLine + Environment.NewLine + ex.Message, appName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            catch (XmlException ex) { MessageBox.Show("Fehler beim Laden der Konfigurationsdatei '" + fileName + "'." + Environment.NewLine + Environment.NewLine + ex.Message, appName, MessageBoxButtons.OK, MessageBoxIcon.Error); }
            foreach (Section section in this.Sections.Values) // Alle Sektionen durchgehen und die Einstellungen einlesen
            {
                foreach (Setting setting in section.Settings.Values) // Alle Einstellungen der Sektion durchlaufen
                {
                    XmlNode settingNode = xmlDoc.SelectSingleNode("/config/" + section.Name + "/" + setting.Name);
                    if (settingNode != null)
                    {// Einstellung gefunden
                        setting.Value = settingNode.InnerText;
                        setting.NotInFile = false;
                    }
                    else
                    {// Einstellung nicht gefunden
                        setting.Value = setting.DefaultValue;
                        setting.NotInFile = true;
                        returnValue = false;
                    }
                }
            }
            return returnValue;
        }

        public void Save() // Speichert die Konfigurationsdaten
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><config></config>");
            foreach (Section section in this.Sections.Values)
            {
                XmlElement sectionElement = xmlDoc.CreateElement(section.Name);
                xmlDoc.DocumentElement.AppendChild(sectionElement);
                foreach (Setting setting in section.Settings.Values)
                {// Einstellungs-Element erzeugen und anfügen
                    XmlElement settingElement = xmlDoc.CreateElement(setting.Name);
                    settingElement.InnerText = setting.Value;
                    sectionElement.AppendChild(settingElement);
                }
            }
            try { xmlDoc.Save(this.fileName); }
            catch (IOException ex) { throw new IOException("Fehler beim Speichern der Konfigurationsdatei '" + this.fileName + "': " + ex.Message); }
            catch (XmlException ex) { throw new XmlException("Fehler beim Speichern der Konfigurationsdatei '" + this.fileName + "': " + ex.Message, ex); }
        }

    }
}
