using System;
using System.Collections.Generic;
using System.Linq;

namespace VaccTests
{
    public class VaccOps
    {

        private List<Doctor> doctors;
        private List<Patient> patients;

        public VaccOps()
        {
            this.doctors = new List<Doctor>();
            this.patients = new List<Patient>();
        }

        public void AddDoctor(Doctor d)
        {
            if (this.doctors.Contains(d))
            {
                throw new ArgumentException();
            }

            this.doctors.Add(d);

        }

        public void AddPatient(Doctor d, Patient p)
        {
            if (!this.doctors.Contains(d))
            {
                throw new ArgumentException();
            }

            //Remove for performance test!!

            //if (this.patients.Contains(p))
            //{
            //    throw new ArgumentException();
            //}

            p.Doctor = d;

            d.Patients.Add(p);

            this.patients.Add(p);
        }

        public IEnumerable<Doctor> GetDoctors()
        {
            return new List<Doctor>(this.doctors);
        }

        public IEnumerable<Patient> GetPatients()
        {
            return new List<Patient>(this.patients);
        }

        public bool Exist(Doctor d)
        {
            return this.doctors.Contains(d);
        }

        public bool Exist(Patient p)
        {
            return this.patients.Contains(p);
        }


        public Doctor RemoveDoctor(string name)
        {
            for (int i = 0; i < this.doctors.Count; i++)
            {
                if (this.doctors[i].Name == name)
                {
                    var current = this.doctors[i];

                    current.Popularity = 0;

                    foreach (var item in current.Patients)
                    {
                        this.patients.Remove(item);                        
                    }

                    this.doctors.Remove(current);

                    return current;
                }
            }

            throw new ArgumentException();
        }

        public void ChangeDoctor(Doctor from, Doctor to, Patient p)
        {
            if (!this.doctors.Contains(from) || !this.doctors.Contains(to) || !this.patients.Contains(p))
            {
                throw new ArgumentException();
            }

            var firstDoctor = this.doctors[this.doctors.IndexOf(from)];
            var secondDoctor = this.doctors[this.doctors.IndexOf(to)];

            secondDoctor.Patients = firstDoctor.Patients;

            foreach (var item in secondDoctor.Patients)
            {
                item.Doctor = secondDoctor;
            }

            firstDoctor.Patients = new List<Patient>();

        }

        public IEnumerable<Doctor> GetDoctorsByPopularity(int populariry)
        {
            var doctorsByPopularity = this.doctors.Where(x => x.Popularity == populariry).ToList();

            return new List<Doctor>(doctorsByPopularity);
        }

        public IEnumerable<Patient> GetPatientsByTown(string town)
        {
            var patientsByTown = this.patients.Where(x => x.Town == town).ToList();

            return new List<Patient>(patientsByTown);
        }

        public IEnumerable<Patient> GetPatientsInAgeRange(int lo, int hi)
        {
            var patientsByAge = this.patients.Where(x => x.Age >= lo && x.Age <= hi).ToList();

            return new List<Patient>(patientsByAge);
        }

        public IEnumerable<Doctor> GetDoctorsSortedByPatientsCountDescAndNameAsc()
        {
            var result = this.doctors.OrderByDescending(x => x.Patients.Count).ThenBy(x => x.Name).ToList();

            return new List<Doctor>(result);
        }


        public IEnumerable<Patient> GetPatientsSortedByDoctorsPopularityAscThenByHeightDescThenByAge()
        {
            var result = this.patients.OrderBy(x => x.Doctor.Popularity).ThenByDescending(x => x.Height).ThenBy(x => x.Age).ToList();

            return new List<Patient>(result);
        }
    }
}
