using System;

namespace Lab2
{
    class Threat 
    {
        public string id { private set; get; } // Идентификатор угрозы
        public string name { private set; get; } // Наименование угрозы
        public string description { private set; get; } // Описание угрозы
        public string sourse { private set; get; } // Источник угрозы
        public string influenceObject { private set; get; } // Объект воздействия
        public bool confidentialityBreach { private set; get; } // Нарушение конфиденциальности
        public bool continuityBreach { private set; get; } // Нарушение целостности
        public bool avaliabilityBreach { private set; get; } // Нарушение доступности
        

        public Threat(string id, string name, string description, string sourse, string influenceObject, string confidentialityBreach, string continuityBreach, string avaliabilityBreach)
        {
            if (int.TryParse(id, out _))
            {
                this.id = "УБИ." + id;
            }
            else
            {
                this.id = id;
            }
             this.name = name; 
            this.description = description;
            if (sourse != "") { this.sourse = sourse;} else { this.sourse = "Unknown"; }
            this.influenceObject = influenceObject;
            this.confidentialityBreach = IntToBool(int.Parse(confidentialityBreach));
            this.continuityBreach = IntToBool(int.Parse(continuityBreach));
            this.avaliabilityBreach= IntToBool(int.Parse(avaliabilityBreach));
            
        }

        public string TxtString(string splitter)
        {
            return string.Join(splitter, new string[] {id, name, description, sourse, influenceObject,
                BoolToInt(confidentialityBreach).ToString(), BoolToInt(continuityBreach).ToString(), BoolToInt(avaliabilityBreach).ToString() });
        }
        public override string ToString()
        {
            return $"Идентификатор: {id}\nНаименование: {name}\nОписание: {description}\nИсточник: {sourse}\nОбъект воздействия: {influenceObject}\n" +
                $"Нарушение конфиденциальности: {confidentialityBreach.ToString()}\nНарушение целостности: {continuityBreach.ToString()}" +
                $"\nНарушение доступности: {avaliabilityBreach.ToString()}";
        }

        private int BoolToInt(bool b)
        {
            if(b == true){return 1;}
            else {return 0;}
        }

        private bool IntToBool(int i)
        {
            if (i == 1) { return true; }
            else { return false; }
        }
        public override bool Equals(object obj)
        {
            Threat t = obj as Threat;
            if (t != null)
            {
                if (this.id == t.id && this.name == t.name && this.description == t.description &&
                    this.sourse == t.sourse && this.influenceObject == t.influenceObject && this.confidentialityBreach == t.confidentialityBreach &&
                    this.continuityBreach == t.continuityBreach && this.avaliabilityBreach == t.avaliabilityBreach)
                {
                    return true;
                }
                
            }
            return false;
        }
    }
}
