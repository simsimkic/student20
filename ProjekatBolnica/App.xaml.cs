using Controller.HospitalController;
using Controller.ManagerController;
using Controller.PatientController;
using Controller.UserController;
using Model;
using Model.DoctorModel;
using Model.ManagerModel;
using Model.PatientModel;
using Model.UtilityModel;
using ProjekatBolnica.Backend.Controller.PatientController;
using ProjekatBolnica.Backend.Controller.UserController;
using ProjekatBolnica.Backend.Model.DoctorModel;
using ProjekatBolnica.Backend.Model.UserModel;
using ProjekatBolnica.Backend.Repository.CSV.Converter.HospitalConverter;
using ProjekatBolnica.Backend.Repository.CSV.Converter.ManagerConverter;
using ProjekatBolnica.Backend.Repository.CSV.Converter.PatientConverter;
using ProjekatBolnica.Backend.Repository.CSV.Converter.UserConverter;
using ProjekatBolnica.Backend.Repository.CSV.Stream;
using ProjekatBolnica.Backend.Repository.HospitalRepository;
using ProjekatBolnica.Backend.Repository.ManagerRepository;
using ProjekatBolnica.Backend.Repository.PatientRepository;
using ProjekatBolnica.Backend.Repository.Sequencer;
using ProjekatBolnica.Backend.Service.PatientServices;
using ProjekatBolnica.Backend.Service.UserService;
using Repository.HospitalRepository;
using Repository.ManagerRepository;
using Repository.PatientRepository;
using Repository.UserRepository;
using Service.HospitalServices;
using Service.ManagerService;
using Service.PatientServices;
using Service.UserService;
using System;
using System.Windows;

namespace ProjekatBolnica
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string INGREDIENTS_FILE = "../../Backend/Data/ingredients.csv";
        private const string MEDICINE_FILE = "../../Backend/Data/medicine.csv";
        private const string UNAPPROVED_MEDICINE_FILE = "../../Backend/Data/unapprovedMedicine.csv";
        private const string RESOURCES_FILE = "../../Backend/Data/resources.csv";
        private const string ROOMS_FILE = "../../Backend/Data/room.csv";
        private const string WORKHOURS_FOR_DOCTOR_FILE = "../../Backend/Data/workhours.csv";
        private const string USER_FILE = "../../Backend/Data/users.csv";
        private const string APPOINTMENT_FILE = "../../Backend/Data/appointment.csv";
        private const string OPERATION_FILE = "../../Backend/Data/operation.csv";
        private const string HOSPITALSTAY_FILE = "../../Backend/Data/hospitalstay.csv";
        private const string PATIENT_FILE = "../../Backend/Data/patient.csv";
        private const string QUESTIONS_AND_ANSWERS_FILE = "../../Backend/Data/questionsAndAnswers.csv";
        private const string MEDICAL_RECORD_FILE = "../../Backend/Data/medicalRecords.csv";
        private const string SURVEY_FILE = "../../Backend/Data/survey.csv";
        private const string FEEDBACK_FILE = "../../Backend/Data/feedback.csv";
        private const string SPECIALTY_FILE = "../../Backend/Data/specialty.csv";
        private const string NOTIFICATION_FILE = "../../Backend/Data/notification.csv";


        private const string CSV_DELIMITER = ",";
        private const string DATETIME_FORMAT = "dd.MM.yyyy.";
        private const string TP_FORMAT = "dd.MM.yyyy HH:mm";
        private const string TIMEPICKER_FORMAT = "dd.MM.yyyy. H:mm";

        public App()
        {
            //REPOSITORY
            var ingredientRepository = new IngredientRepository(
              new CSVStream<Ingredient>(INGREDIENTS_FILE, new IngredientsCSVConverter(CSV_DELIMITER)),
              new LongSequencer());
            var medicineRepository = new MedicineRepository(
              new CSVStream<Medicine>(MEDICINE_FILE, new MedicineCSVConverter(CSV_DELIMITER)),
              new LongSequencer(), ingredientRepository);
            var unapprovedMedicineRepository = new UnapprovedMedicineRepository(
              new CSVStream<Medicine>(UNAPPROVED_MEDICINE_FILE, new MedicineCSVConverter(CSV_DELIMITER)),
              new LongSequencer(), ingredientRepository);
            var roomsRepository = new RoomsRepository(
             new CSVStream<Room>(ROOMS_FILE, new RoomsCSVConverter(CSV_DELIMITER, TP_FORMAT)),
             new LongSequencer());
            var resourceRepository = new ResourceRepository(
              new CSVStream<Resource>(RESOURCES_FILE, new ResourcesCSVConverter(CSV_DELIMITER)),
              new LongSequencer(), roomsRepository);
            var workHoursForDoctorRepository = new WorkHoursForDoctorRepository(
              new CSVStream<WorkHoursForDoctor>(WORKHOURS_FOR_DOCTOR_FILE, new WorkHoursForDoctorCSVConverter(CSV_DELIMITER, TP_FORMAT)),
              new LongSequencer());
            var feedbackRepository = new FeedbackRepository(
               new CSVStream<Feedback>(FEEDBACK_FILE, new FeedbackCSVConverter(CSV_DELIMITER)),
               new LongSequencer());
            var specialtyRepository = new SpecialtyRepository(
                new CSVStream<Specialty>(SPECIALTY_FILE, new SpecialtyCSVConverter(CSV_DELIMITER)),
                new LongSequencer());
            var userRepository = new UserRepository(
               new CSVStream<User>(USER_FILE, new UserCSVConverter(CSV_DELIMITER, DATETIME_FORMAT)),
               new LongSequencer(),
               specialtyRepository);
            var appointmentRepository = new AppointmentRepository(
               new CSVStream<Appointment>(APPOINTMENT_FILE, new AppointmentCSVConverter(CSV_DELIMITER, TIMEPICKER_FORMAT)),
               new LongSequencer(),
               roomsRepository,
               userRepository);
            var operationRepository = new OperationRepository(
               new CSVStream<Operation>(OPERATION_FILE, new OperationCSVConverter(CSV_DELIMITER, TIMEPICKER_FORMAT)),
               new LongSequencer(),
               roomsRepository,
               userRepository);
           var hospitalStayRepository = new HospitalStayRepository(
               new CSVStream<HospitalStay>(HOSPITALSTAY_FILE, new HospitalStayCSVConverter(CSV_DELIMITER, DATETIME_FORMAT)),
               new LongSequencer(),
               userRepository,
               resourceRepository);


            var questionsAndAnswersRepository = new QuestionsAndAnswersRepository(
                new CSVStream<QuestionsAndAnswers>(QUESTIONS_AND_ANSWERS_FILE, new QuestionsAndAnswersCSVConverter(CSV_DELIMITER)),
                new LongSequencer());
            var medicalRecordRepository = new MedicalRecordRepository(
                  new CSVStream<MedicalRecord>(MEDICAL_RECORD_FILE, new MedicalRecordCSVConverter(CSV_DELIMITER, DATETIME_FORMAT)),
                  new LongSequencer(),
                  userRepository, medicineRepository);
            var surveyRepository = new SurveyRepository(
                new CSVStream<Survey>(SURVEY_FILE, new SurveyCSVConverter(CSV_DELIMITER)),
                new LongSequencer(),
                questionsAndAnswersRepository);
            var notificationRepository = new NotificationRepository(
                new CSVStream<Notification>(NOTIFICATION_FILE, new NotificationCSVConverter(CSV_DELIMITER, TIMEPICKER_FORMAT)), new LongSequencer(), userRepository);


            //SERVICE
            var notificationService = new NotificationService(notificationRepository, appointmentRepository);
            var userService = new UserService(userRepository, feedbackRepository);
            var ingredientService = new IngredientService(ingredientRepository);
            var medicineService = new MedicineService(medicineRepository, unapprovedMedicineRepository, ingredientService, userService);
            var roomService = new RoomService(roomsRepository, notificationService);
            var resourceService = new ResourceService(resourceRepository, roomService);
            var workHoursForDoctorService = new WorkHoursForDoctorService(workHoursForDoctorRepository);
            var appointmentService = new AppointmentService(appointmentRepository,roomsRepository, userRepository, workHoursForDoctorService);
            var hospitalStayService = new HospitalStayService(hospitalStayRepository);
            var operationService = new OperationService(operationRepository, workHoursForDoctorService);
            //var patientService = new PatientService(patientRepository);
            var medicalRecordService = new MedicalRecordService(medicalRecordRepository);
            var surveyService = new SurveyService(surveyRepository);
            var questionsAndAnswersService = new QuestionsAndAnswersService(questionsAndAnswersRepository);
            var approvalOfMedicineService = new ApprovalOfMedicineService(notificationRepository);

            //CONTROLLER
            IngredientController = new IngredientController(ingredientService);
            MedicineController = new MedicineController(medicineService);
            ResourceController = new ResourceController(resourceService);
            RoomsController = new RoomsController(roomService);
            WorkHoursForDoctorController = new WorkHoursForDoctorController(workHoursForDoctorService);
            UserController = new UserController(userService);
            AppointmentController = new AppointmentController(appointmentService);
            OperationController = new OperationController(operationService);
            HospitalStayController = new HospitalStayController(hospitalStayService);
            //PatientController = new PatientController(patientService);
            MedicalRecordController = new MedicalRecordController(medicalRecordService);
            SurveyController = new SurveyController(surveyService);
            QuestionsAndAnswersController = new QuestionsAndAnwsersController(questionsAndAnswersService);
            NotificationController = new NotificationController(notificationService);
            ApprovalOfMedicineController = new ApprovalOfMedicineController(approvalOfMedicineService);
        }


        public IController<Ingredient, long> IngredientController { get; private set; }
        public IMedicineController MedicineController { get; private set; }
        public IResourceController ResourceController { get; private set; }
        public IRoomsController RoomsController { get; private set; }
        public IWorkHoursForDoctorController WorkHoursForDoctorController { get; private set; }
        public IUserController UserController { get; private set; }
        public IAppointmentController AppointmentController { get; private set; }
        public IOperationController OperationController { get; private set; }
        public IHospitalStayController HospitalStayController { get; private set; }
        public IMedicalRecordsController MedicalRecordController { get; private set; }
        public IController<Survey, long> SurveyController { get; private set; }
        public IController<QuestionsAndAnswers, long> QuestionsAndAnswersController { get; private set; }
        public INotificationController NotificationController { get; private set; }
        public IApprovalOfMedicineController ApprovalOfMedicineController { get; private set; }
    }
}
