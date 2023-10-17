namespace AnarchyInTheHospital;
class Program
{
    static void Main(string[] args)
    {
        List<Patient> patients = new List<Patient>
        {
            new ("Данилова А.Я.", 50, Diseases.GamingAddiction),
            new ("Волкова Е.Д.", 23, Diseases.Xerophthalmia),
            new ("Савельева Е.С.", 42, Diseases.CarpalTunnelsyndrome),
            new ("Никитина К.А.", 28, Diseases.Xerophthalmia),
            new ("Попова С.М.", 19, Diseases.GamingAddiction),
            new ("Васильева А.О.", 72, Diseases.CarpalTunnelsyndrome),
            new ("Тихонова В.Т.", 32, Diseases.Xerophthalmia),
            new ("Ефимова С.Ю.", 38, Diseases.GamingAddiction),
            new ("Карпов З.Д.", 22, Diseases.GamingAddiction),
            new ("Сомов А.А.", 34, Diseases.CarpalTunnelsyndrome)
        };

        Hospital hospital = new Hospital(patients);
        
        hospital.Work();
    }
}

public class Diseases
{
    public const string GamingAddiction = "игровая зависимость";
    public const string CarpalTunnelsyndrome = "туннельный синдром запястья";
    public const string Xerophthalmia = "Ксерофтальмия";
}

class Patient
{
    public Patient(string name, int age, string disease)
    {
        Name = name;
        Age = age;
        Disease = disease;
    }
    
    public string Name { get; private set; }
    public int Age { get; private set; }
    public string Disease { get; private set; }
}

class Hospital
{
    private List<Patient> _patients;

    public Hospital(List<Patient> patients)
    {
        _patients = patients;
    }

    public void Work()
    {
        bool isWorking = true;

        while (isWorking)
        {
            const char SortByNameCommand = '1';
            const char SortByAgeCommand = '2';
            const char SortByDiseaseCommand = '3';
            const char ExitCommand = 'q';
            
            Console.Clear();
            Console.WriteLine("Картотека поликлиники\n\n" + 
                              $"{SortByNameCommand} - вывести пациентов в алфавитном порядке\n" +
                              $"{SortByAgeCommand} - вывести пациентов в порядке возвраста\n" +
                              $"{SortByDiseaseCommand} - вывести пациентов с определённым диакнозом\n" +
                              $"{ExitCommand} - выход из программы\n");

            switch (Console.ReadKey(true).KeyChar)
            {
                case SortByNameCommand:
                    SortByName();
                    break;
                
                case SortByAgeCommand:
                    SortByAge();
                    break;
                
                case SortByDiseaseCommand:
                    SortByDisease();
                    break;
                
                case ExitCommand:
                    isWorking = false;
                    break;
            }
            
            if (isWorking == false)
                continue;
            
            Console.Write("\nНажмите любую кнопку для продолжения...");
            Console.ReadKey();
        }
    }

    private void SortByName()
    {
        List<Patient> sortedPatients = _patients.OrderBy(patient => patient.Name).ToList();
        
        Console.WriteLine("Список пациентов в алфавитном порядке:\n");
        ShowPatientsInfo(sortedPatients);
    }

    private void SortByAge()
    {
        List<Patient> sortedPatients = _patients.OrderBy(patient => patient.Age).ToList();
        
        Console.WriteLine("Список пациентов, от младшего к старшему:\n");
        ShowPatientsInfo(sortedPatients);
    }

    private void SortByDisease()
    {
        List<Patient> sortedPatients;
        string disease;
        
        Console.Write("Введите диагноз: ");
        disease = HandleInput();
        
        sortedPatients = _patients.Where(patient => patient.Disease == disease).ToList();
        
        ShowPatientsInfo(sortedPatients);
    }

    private string HandleInput()
    {
        string userInput = string.Empty;
        bool isCorrect = false;
        
        while (isCorrect == false)
        {
            userInput = Console.ReadLine().Trim().ToLower();

            if (userInput == string.Empty)
                Console.Write("Диагноз не может быть пустым. Попробуйте ещё: ");
            else
                isCorrect = true;
        }

        return userInput;
    }

    private void ShowPatientsInfo(List<Patient> patients)
    {
        if (patients.Count == 0)
        {
            Console.WriteLine("Пациентов не найдено.");
            return;
        }
    
        foreach(Patient patient in patients)
            Console.WriteLine($"{patient.Name}. Возраст: {patient.Age}. Диагноз: {patient.Disease}.");
    }
}