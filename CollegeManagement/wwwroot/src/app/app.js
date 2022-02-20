//angular APP
var app = angular.module("CollegeManagementApp", []);

// ---- Courses and Subjects Controller ---- //
app.controller("CoursesCtrl", function ($scope, $http) {

    // -- Courses --//

    // GET ALL
    $scope.GetAllDataCourses = function () {

        $http({
            method: "get",
            url: '/Courses/GetAllCourses/'
        }).then(function (response) {
            $scope.courses = response.data;
        }, function () {
            $('#form-toast .toast-title').html("Error!")
            $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
            $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
            $('#form-toast-error').toast('show');
        })
    };

    //GET UPDATE
    $scope.GetUpdateCourse = function (Course) {
        document.getElementById("CourseID_").value = Course.idCourse;
        $scope.CourseName = Course.dsCourse;
        document.getElementById("btnSave-course").setAttribute("value", "Update");
        document.getElementById("modal-title-course").innerHTML = "Update Course";
        $('#crud-modal-course').modal('show');
    };

    //GET INSERT
    $scope.GetInsertCourse = function () {
        $scope.CourseName = "";
        document.getElementById("btnSave-course").setAttribute("value", "Submit");
        document.getElementById("modal-title-course").innerHTML = "Insert Course";
        $('#crud-modal-course').modal('show');
    };

    //GET DELETE
    $scope.GetDeleteCourse = function (Course) {
        document.getElementById("CourseID_").value = Course.idCourse;
        var btn = document.getElementById("btnDeleteModal-course");
        btn.setAttribute("value", "Delete Course");
        document.getElementById("modal-delete-course-title").innerHTML = "Delete Course";
        $scope.typeOfEntity = "course";
        $('#delete-modal-course').modal('show');
    };

    //POST INSERT/UPDATE
    $scope.InsertDataCourse = function () {

        //INSERT
        var Action = document.getElementById("btnSave-course").getAttribute("value");
        if (Action == "Submit") {
            $scope.Course = {};
            $scope.Course.DsCourse = $scope.CourseName;

            $http({
                method: "post",
                url: '/Courses/InsertCourse/',
                datatype: "json",
                data: JSON.stringify($scope.Course)
            }).then(function (response) {
                $scope.GetAllDataCourses();
                $scope.CourseName = "";
                $('#crud-modal-course').modal('hide');

                //TYPE OF TOAST
                var toastType = response.data.toString().substring(0, response.data.toString().indexOf("!"));
                if (toastType == "Sucess") {
                    $('#form-toast .toast-title').html("Sucess!");
                    $('#form-toast').css({ 'background-color': 'MediumSeaGreen', 'color': 'white' });
                    $('#toast-sucess-body').html(response.data)
                }
                else {
                    $('#form-toast .toast-title').html("Error!")
                    $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                    $('#toast-sucess-body').html(response.data)
                }
                $('#form-toast').toast('show');

                //INTERNAL ERROR
            }, function () {
                $('#form-toast .toast-title').html("Error!")
                $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
                $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                $('#form-toast-error').toast('show');
            })
        } else {

            //UPDATE
            $scope.Course = {};
            $scope.Course.DsCourse = $scope.CourseName;

            $scope.Course.IdCourse = document.getElementById("CourseID_").value;
            $http({
                method: "post",
                url: "/Courses/UpdateCourse/",
                datatype: "json",
                data: JSON.stringify($scope.Course)
            }).then(function (response) {
                $scope.GetAllDataCourses();
                $scope.CourseName = "";
                $('#crud-modal-course').modal('hide');

                //TYPE OF TOAST
                var toastType = response.data.toString().substring(0, response.data.toString().indexOf("!"));
                if (toastType == "Sucess") {
                    $('#form-toast .toast-title').html("Sucess!");
                    $('#form-toast').css({ 'background-color': 'MediumSeaGreen', 'color': 'white' });
                    $('#toast-sucess-body').html(response.data)
                }
                else {
                    $('#form-toast .toast-title').html("Error!")
                    $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                    $('#toast-sucess-body').html(response.data)
                }
                $('#form-toast').toast('show');

                document.getElementById("btnSave-course").setAttribute("value", "Submit");
                document.getElementById("modal-title-course").innerHTML = "Add New Course";

                //INTERNAL ERROR
            }, function () {
                $('#form-toast .toast-title').html("Error!")
                $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
                $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                $('#form-toast-error').toast('show');
            })
        }
    }

    //POST DELETE
    $scope.DeleteCourse = function () {

        $scope.Course = {};
        $scope.Course.IdCourse = document.getElementById("CourseID_").value;

        $http({
            method: "post",
            url: '/Courses/DeleteCourse/',
            datatype: "json",
            data: JSON.stringify($scope.Course)
        }).then(function (response) {
            //TYPE OF TOAST
            $('#delete-modal-course').modal('hide');

            var toastType = response.data.toString().substring(0, response.data.toString().indexOf("!"));
            if (toastType == "Sucess") {
                $('#form-toast .toast-title').html("Sucess!");
                $('#form-toast').css({ 'background-color': 'MediumSeaGreen', 'color': 'white' });
                $('#toast-sucess-body').html(response.data)
            }
            else {
                $('#form-toast .toast-title').html("Error!")
                $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                $('#toast-sucess-body').html(response.data)
            }
            $('#form-toast').toast('show');

            $scope.GetAllDataCourses();

            //INTERNAL ERROR
        }, function () {
            $('#form-toast .toast-title').html("Error!")
            $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
            $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
            $('#form-toast-error').toast('show');
        })
    }


    // -------- Subjects -------//

    //Get Id Course by Subject
    $scope.GetIdCourseSubject = function (Course) {
        document.getElementById("CourseID_").value = Course.idCourse;
        document.getElementById("subjectArea").style.display = "block";
        $scope.GetAllDataSubjects();
    };

    // GET ALL SUBJECTS
    $scope.GetAllDataSubjects = function () {
        $http({
            method: "get",
            url: '/Subjects/GetAllSubjects?idCourse=' + document.getElementById("CourseID_").value
        }).then(function (response) {
            $scope.subjects = response.data;
        }, function () {
            $('#form-toast .toast-title').html("Error!")
            $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
            $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
            $('#form-toast-error').toast('show');
        })
    };

    // GET Teachers
    $scope.GetAllTeachers = function () {
        $http({
            method: "get",
            url: '/Teachers/GetAllTeachers/'
        }).then(function (response) {
            $scope.teachers = response.data;
        }, function () {
            $('#form-toast .toast-title').html("Error!")
            $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
            $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
            $('#form-toast-error').toast('show');
        })
    };

    //GET UPDATE
    $scope.GetUpdateSubject = function (Subject) {
        $scope.SubjectTeacher = "";
        document.getElementById("SubjectID_").value = Subject.idSubject;
        $scope.SubjectName = Subject.dsSubject;
        $scope.SubjectTeacher = Subject.idTeacher.toString();
        document.getElementById("btnSave-subject").setAttribute("value", "Update");
        document.getElementById("modal-title-subject").innerHTML = "Update Subject";
        $('#crud-modal-subject').modal('show');
    };

    //GET INSERT
    $scope.GetInsertSubject = function () {
        $scope.SubjectName = "";
        $scope.SubjectTeacher = "";
        document.getElementById("btnSave-subject").setAttribute("value", "Submit");
        document.getElementById("modal-title-subject").innerHTML = "Insert Subject";
        $('#crud-modal-subject').modal('show');
    };

    //GET DELETE
    $scope.GetDeleteSubject = function (Subject) {
        document.getElementById("SubjectID_").value = Subject.idSubject;
        var btn = document.getElementById("btnDeleteModal-subject");
        btn.setAttribute("value", "Delete Subject");
        document.getElementById("modal-delete-subject-title").innerHTML = "Delete Subject";
        $scope.typeOfEntity = "subject";
        $('#delete-modal-subject').modal('show');
    };

    //POST INSERT/UPDATE
    $scope.InsertDataSubject = function () {

        //INSERT
        var Action = document.getElementById("btnSave-subject").getAttribute("value");
        if (Action == "Submit") {
            $scope.Subject = {};
            $scope.Subject.DsSubject = $scope.SubjectName;
            $scope.Subject.IdTeacher = $scope.SubjectTeacher;
            $scope.Subject.IdCourse = document.getElementById("CourseID_").value;
            var content = JSON.stringify($scope.Subject);
            $http({
                method: "post",
                url: '/Subjects/InsertSubject/',
                datatype: "json",
                data: JSON.stringify($scope.Subject)
            }).then(function (response) {
                $scope.GetAllDataSubjects();
                $scope.GetAllDataCourses();
                $scope.SubjectName = "";
                $('#crud-modal-subject').modal('hide');

                //TYPE OF TOAST
                var toastType = response.data.toString().substring(0, response.data.toString().indexOf("!"));
                if (toastType == "Sucess") {
                    $('#form-toast .toast-title').html("Sucess!");
                    $('#form-toast').css({ 'background-color': 'MediumSeaGreen', 'color': 'white' });
                    $('#toast-sucess-body').html(response.data)
                }
                else {
                    $('#form-toast .toast-title').html("Error!")
                    $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                    $('#toast-sucess-body').html(response.data)
                }
                $('#form-toast').toast('show');

                //INTERNAL ERROR
            }, function () {
                $('#form-toast .toast-title').html("Error!")
                $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
                $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                $('#form-toast-error').toast('show');
            })
        } else {
            //UPDATE
            $scope.Subject = {};
            $scope.Subject.DsSubject = $scope.SubjectName;
            $scope.Subject.IdTeacher = $scope.SubjectTeacher;
            $scope.Subject.IdCourse = document.getElementById("CourseID_").value;
            $scope.Subject.IdSubject = document.getElementById("SubjectID_").value;

            $http({
                method: "post",
                url: "/Subjects/UpdateSubject/",
                datatype: "json",
                data: JSON.stringify($scope.Subject)
            }).then(function (response) {
                $scope.GetAllDataSubjects();
                $scope.GetAllDataCourses();
                $scope.SubjectName = "";
                $('#crud-modal-subject').modal('hide');

                //TYPE OF TOAST
                var toastType = response.data.toString().substring(0, response.data.toString().indexOf("!"));
                if (toastType == "Sucess") {
                    $('#form-toast .toast-title').html("Sucess!");
                    $('#form-toast').css({ 'background-color': 'MediumSeaGreen', 'color': 'white' });
                    $('#toast-sucess-body').html(response.data)
                }
                else {
                    $('#form-toast .toast-title').html("Error!")
                    $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                    $('#toast-sucess-body').html(response.data)
                }
                $('#form-toast').toast('show');

                document.getElementById("btnSave-subject").setAttribute("value", "Submit");
                document.getElementById("modal-title").innerHTML = "Add New Subject";

                //INTERNAL ERROR
            }, function () {
                $('#form-toast .toast-title').html("Error!")
                $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
                $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                $('#form-toast-error').toast('show');
            })
        }
    }

    //POST DELETE
    $scope.DeleteSubject = function () {

        $scope.Subject = {};
        $scope.Subject.IdSubject = document.getElementById("SubjectID_").value;

        $http({
            method: "post",
            url: '/Subjects/DeleteSubject/',
            datatype: "json",
            data: JSON.stringify($scope.Subject)
        }).then(function (response) {
            //TYPE OF TOAST
            $('#delete-modal-subject').modal('hide');

            var toastType = response.data.toString().substring(0, response.data.toString().indexOf("!"));
            if (toastType == "Sucess") {
                $('#form-toast .toast-title').html("Sucess!");
                $('#form-toast').css({ 'background-color': 'MediumSeaGreen', 'color': 'white' });
                $('#toast-sucess-body').html(response.data)
            }
            else {
                $('#form-toast .toast-title').html("Error!")
                $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                $('#toast-sucess-body').html(response.data)
            }
            $('#form-toast').toast('show');

            $scope.GetAllDataSubjects();

            //INTERNAL ERROR
        }, function () {
            $('#form-toast .toast-title').html("Error!")
            $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
            $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
            $('#form-toast-error').toast('show');
        })
    };

    // -------- Students -------//

    //GET Subjects by Course
    $scope.GetSubjectsCourse = function () {
        document.getElementById("CourseID_").value = $scope.CourseSelection;
        $scope.GetAllDataSubjects();
    }

    //GET Students by Course
    $scope.GetAllStundetsCourse = function () {
        //List Students on Course
        $http({
            method: "get",
            url: '/Students/GetStudentsByCourse?idCourse=' + document.getElementById("CourseID_").value + "&idSubject=" + document.getElementById("SubjectID_").value
        }).then(function (response) {
            $scope.studentsCourses = response.data;
        }, function () {
            $('#form-toast .toast-title').html("Error!")
            $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
            $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
            $('#form-toast-error').toast('show');
        })
    }

    //SET Ids On subjects
    $scope.GetStudentsSubject = function (Subject) {
        document.getElementById("subjectDesc").innerHTML = "Students in " + Subject.dsSubject;
        document.getElementById("studentsArea").style.display = "block";
        document.getElementById("SubjectID_").value = Subject.idSubject;
        $scope.ListStudentsSubject();
    }

    //GET Students by Subject
    $scope.ListStudentsSubject = function () {
        $http({
            method: "get",
            url: '/Students/GetStudentsBySubject?idSubject=' + document.getElementById("SubjectID_").value
        }).then(function (response) {
            $scope.studentsSubjects = response.data;
        }, function () {
            $('#form-toast .toast-title').html("Error!")
            $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
            $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
            $('#form-toast-error').toast('show');
        })
    };

    //GET Insert Student on Subject
    $scope.GetInsertStudent = function () {
        document.getElementById("CourseID_").value = $scope.CourseSelection;
        $scope.GetAllStundetsCourse();
        $scope.StudentSubjectName = "";
        $scope.StudentSubjectGrade = "";
        $('#crud-modal-studentSubject').modal('show');

    };

    //GET Remove From
    $scope.GetRemoveFromSubject = function (studentSubject) {
        document.getElementById("StudentID_").value = studentSubject.idStudentRegistrationNumber;
        $('#remove-modal-student').modal('show');
    }

    //POST Insert Student From Subject
    $scope.InsertStudentSubject = function () {

        $scope.Student = {};
        $scope.Student.idStudentRegistrationNumber = $scope.StudentSubjectName;
        $scope.Student.idSubject = document.getElementById("SubjectID_").value;
        $scope.Student.Grade = $scope.StudentSubjectGrade

        $http({
            method: "post",
            url: '/Students/InsertStudentOnSubject/',
            datatype: "json",
            data: JSON.stringify($scope.Student)
        }).then(function (response) {
            //TYPE OF TOAST
            $('#crud-modal-studentSubject').modal('hide');

            var toastType = response.data.toString().substring(0, response.data.toString().indexOf("!"));
            if (toastType == "Sucess") {
                $('#form-toast .toast-title').html("Sucess!");
                $('#form-toast').css({ 'background-color': 'MediumSeaGreen', 'color': 'white' });
                $('#toast-sucess-body').html(response.data)
            }
            else {
                $('#form-toast .toast-title').html("Error!")
                $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                $('#toast-sucess-body').html(response.data)
            }
            $('#form-toast').toast('show');

            $scope.ListStudentsSubject();
            $scope.GetSubjectsCourse();

            //INTERNAL ERROR
        }, function () {
            $('#form-toast .toast-title').html("Error!")
            $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
            $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
            $('#form-toast-error').toast('show');
        })
    };

    //POST Remove Student From Subject
    $scope.RemoveStudentSubject = function () {

        $scope.Student = {};
        $scope.Student.idStudentRegistrationNumber = document.getElementById("StudentID_").value;
        $scope.Student.idSubject = document.getElementById("SubjectID_").value;

        $http({
            method: "post",
            url: '/Students/RemoveStudentFromSubject/',
            datatype: "json",
            data: JSON.stringify($scope.Student)
        }).then(function (response) {
            //TYPE OF TOAST
            $('#remove-modal-student').modal('hide');

            var toastType = response.data.toString().substring(0, response.data.toString().indexOf("!"));
            if (toastType == "Sucess") {
                $('#form-toast .toast-title').html("Sucess!");
                $('#form-toast').css({ 'background-color': 'MediumSeaGreen', 'color': 'white' });
                $('#toast-sucess-body').html(response.data)
            }
            else {
                $('#form-toast .toast-title').html("Error!")
                $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                $('#toast-sucess-body').html(response.data)
            }
            $('#form-toast').toast('show');

            $scope.ListStudentsSubject();
            $scope.GetSubjectsCourse();


            //INTERNAL ERROR
        }, function () {
            $('#form-toast .toast-title').html("Error!")
            $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
            $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
            $('#form-toast-error').toast('show');
        })
    };
});

// ---- Teachers ---- //
app.controller("TeachersCtrl", function ($scope, $http) {

    // GET ALL
    $scope.GetAllDataTeachers = function () {
        $http({
            method: "get",
            url: '/Teachers/GetAllTeachers/'
        }).then(function (response) {
            $scope.teachers = response.data;
        }, function () {
            $('#form-toast .toast-title').html("Error!")
            $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
            $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
            $('#form-toast-error').toast('show');
        })
    };

    //GET UPDATE
    $scope.GetUpdateTeacher = function (Teacher) {

        //Set ID
        document.getElementById("TeacherID_").value = Teacher.idTeacher;

        //Set Teacher info
        $scope.TeacherName = Teacher.name;
        var BirthdayDate = Teacher.birthday.toString().substring(0, Teacher.birthday.toString().indexOf("T"));
        document.getElementById("inputBirthday").value = BirthdayDate;
        $scope.TeacherSalary = Teacher.salary;

        //Change button action
        document.getElementById("btnSave-teacher").setAttribute("value", "Update");

        //Show modal
        document.getElementById("modal-title").innerHTML = "Update Teacher";
        $('#crud-modal-teacher').modal('show');
    };

    //GET INSERT
    $scope.GetInsertTeacher = function () {
        $scope.TeacherName = "";
        $scope.TeacherBirthday = "";
        $scope.TeacherSalary = "";
        document.getElementById("btnSave-teacher").setAttribute("value", "Submit");
        document.getElementById("modal-title").innerHTML = "Insert Teacher";
        $('#crud-modal-teacher').modal('show');
    };

    //GET DELETE
    $scope.GetDeleteTeacher = function (Teacher) {
        document.getElementById("TeacherID_").value = Teacher.idTeacher;
        var btn = document.getElementById("btnDeleteModal-teacher");
        btn.setAttribute("value", "Delete Teacher");
        document.getElementById("modal-delete-title").innerHTML = "Delete Teacher";
        $scope.typeOfEntity = "teacher";
        $('#delete-modal-teacher').modal('show');
    };

    //POST INSERT/UPDATE
    $scope.InsertData = function () {

        //INSERT
        var Action = document.getElementById("btnSave-teacher").getAttribute("value");
        if (Action == "Submit") {
            $scope.Teacher = {};
            $scope.Teacher.name = $scope.TeacherName;
            $scope.Teacher.Birthday = $scope.TeacherBirthday;
            $scope.Teacher.Salary = $scope.TeacherSalary;
            var content = JSON.stringify($scope.Teacher);
            $http({
                method: "post",
                url: '/Teachers/InsertTeacher/',
                datatype: "json",
                data: JSON.stringify($scope.Teacher)
            }).then(function (response) {
                $scope.GetAllDataTeachers();
                //Clean Previous selections
                $scope.TeacherName = "";
                $scope.TeacherSalary = "";
                document.getElementById("inputBirthday").value = "";
                //Hide Modal
                $('#crud-modal-teacher').modal('hide');

                //TYPE OF TOAST
                var toastType = response.data.toString().substring(0, response.data.toString().indexOf("!"));
                if (toastType == "Sucess") {
                    $('#form-toast .toast-title').html("Sucess!");
                    $('#form-toast').css({ 'background-color': 'MediumSeaGreen', 'color': 'white' });
                    $('#toast-sucess-body').html(response.data)
                }
                else {
                    $('#form-toast .toast-title').html("Error!")
                    $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                    $('#toast-sucess-body').html(response.data)
                }
                $('#form-toast').toast('show');

                //INTERNAL ERROR
            }, function () {
                $('#form-toast .toast-title').html("Error!")
                $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
                $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                $('#form-toast-error').toast('show');
            })
        } else {

            //UPDATE
            $scope.Teacher = {};
            $scope.Teacher.name = $scope.TeacherName;
            $scope.Teacher.Birthday = $scope.TeacherBirthday;
            $scope.Teacher.Salary = $scope.TeacherSalary;

            $scope.Teacher.IdTeacher = document.getElementById("TeacherID_").value;
            $http({
                method: "post",
                url: "/Teachers/UpdateTeacher/",
                datatype: "json",
                data: JSON.stringify($scope.Teacher)
            }).then(function (response) {
                $scope.GetAllDataTeachers();
                //Clean Previous selections
                $scope.TeacherName = "";
                $scope.TeacherSalary = "";
                document.getElementById("inputBirthday").value = "";
                //Hide Modal
                $('#crud-modal-teacher').modal('hide');

                //TYPE OF TOAST
                var toastType = response.data.toString().substring(0, response.data.toString().indexOf("!"));
                if (toastType == "Sucess") {
                    $('#form-toast .toast-title').html("Sucess!");
                    $('#form-toast').css({ 'background-color': 'MediumSeaGreen', 'color': 'white' });
                    $('#toast-sucess-body').html(response.data)
                }
                else {
                    $('#form-toast .toast-title').html("Error!")
                    $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                    $('#toast-sucess-body').html(response.data)
                }
                $('#form-toast').toast('show');

                document.getElementById("btnSave-teacher").setAttribute("value", "Submit");
                document.getElementById("modal-title").innerHTML = "Add New Teacher";

                //INTERNAL ERROR
            }, function () {
                $('#form-toast .toast-title').html("Error!")
                $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
                $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                $('#form-toast-error').toast('show');
            })
        }
    }

    //POST DELETE
    $scope.DeleteTeacher = function () {

        $scope.Teacher = {};
        $scope.Teacher.IdTeacher = document.getElementById("TeacherID_").value;

        $http({
            method: "post",
            url: '/Teachers/DeleteTeacher/',
            datatype: "json",
            data: JSON.stringify($scope.Teacher)
        }).then(function (response) {
            //TYPE OF TOAST
            $('#delete-modal-teacher').modal('hide');

            var toastType = response.data.toString().substring(0, response.data.toString().indexOf("!"));
            if (toastType == "Sucess") {
                $('#form-toast .toast-title').html("Sucess!");
                $('#form-toast').css({ 'background-color': 'MediumSeaGreen', 'color': 'white' });
                $('#toast-sucess-body').html(response.data)
            }
            else {
                $('#form-toast .toast-title').html("Error!")
                $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                $('#toast-sucess-body').html(response.data)
            }
            $('#form-toast').toast('show');

            $scope.GetAllDataTeachers();

            //INTERNAL ERROR
        }, function () {
            $('#form-toast .toast-title').html("Error!")
            $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
            $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
            $('#form-toast-error').toast('show');
        })

    }
});


// ---- Students ---- //

app.controller("StudentsCtrl", function ($scope, $http) {

    // GET ALL
    $scope.GetAllDataStudents = function () {
        $http({
            method: "get",
            url: '/Students/GetAllStudents/'
        }).then(function (response) {
            $scope.students = response.data;
        }, function () {
            $('#form-toast .toast-title').html("Error!")
            $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
            $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
            $('#form-toast-error').toast('show');
        })
    };

    // GET ALL
    $scope.GetAllDataCourses = function () {
        $http({
            method: "get",
            url: '/Courses/GetAllCourses/'
        }).then(function (response) {
            $scope.courses = response.data;
        }, function () {
            $('#form-toast .toast-title').html("Error!")
            $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
            $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
            $('#form-toast-error').toast('show');
        })
    };

    //GET UPDATE
    $scope.GetUpdateStudent = function (Student) {

        //Set ID
        document.getElementById("StudentID_").value = Student.idStudentRegistrationNumber;

        //Set Student info
        $scope.StudentName = Student.name;
        var BirthdayDate = Student.birthday.toString().substring(0, Student.birthday.toString().indexOf("T"));
        document.getElementById("inputBirthday").value = BirthdayDate;
        $scope.StudentCourse = Student.idCourse.toString();

        //Change button action
        document.getElementById("btnSave-student").setAttribute("value", "Update");

        //Show modal
        document.getElementById("modal-title").innerHTML = "Update Student";
        $('#crud-modal-student').modal('show');
    };

    //GET INSERT
    $scope.GetInsertStudent = function () {
        $scope.StudentName = "";
        $scope.StudentBirthday = "";
        $scope.StudentCourse = "";
        document.getElementById("btnSave-student").setAttribute("value", "Submit");
        document.getElementById("modal-title").innerHTML = "Insert Student";
        $('#crud-modal-student').modal('show');
    };

    //GET DELETE
    $scope.GetDeleteStudent = function (Student) {
        document.getElementById("StudentID_").value = Student.idStudentRegistrationNumber;
        var btn = document.getElementById("btnDeleteModal-student");
        btn.setAttribute("value", "Delete Student");
        document.getElementById("modal-delete-title").innerHTML = "Delete Student";
        $scope.typeOfEntity = "student";
        $('#delete-modal-student').modal('show');
    };

    //POST INSERT/UPDATE
    $scope.InsertData = function () {

        //INSERT
        var Action = document.getElementById("btnSave-student").getAttribute("value");
        if (Action == "Submit") {
            $scope.Student = {};
            $scope.Student.name = $scope.StudentName;
            $scope.Student.Birthday = $scope.StudentBirthday;
            $scope.Student.idCourse = $scope.StudentCourse;

            var content = JSON.stringify($scope.Student);

            $http({
                method: "post",
                url: '/Students/InsertStudent/',
                datatype: "json",
                data: JSON.stringify($scope.Student)
            }).then(function (response) {
                $scope.GetAllDataStudents();
                //Clean Previous selections
                $scope.StudentName = "";
                $scope.StudentCourse = "";
                document.getElementById("inputBirthday").value = "";
                //Hide Modal
                $('#crud-modal-student').modal('hide');

                //TYPE OF TOAST
                var toastType = response.data.toString().substring(0, response.data.toString().indexOf("!"));
                if (toastType == "Sucess") {
                    $('#form-toast .toast-title').html("Sucess!");
                    $('#form-toast').css({ 'background-color': 'MediumSeaGreen', 'color': 'white' });
                    $('#toast-sucess-body').html(response.data)
                }
                else {
                    $('#form-toast .toast-title').html("Error!")
                    $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                    $('#toast-sucess-body').html(response.data)
                }
                $('#form-toast').toast('show');

                //INTERNAL ERROR
            }, function () {
                $('#form-toast .toast-title').html("Error!")
                $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
                $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                $('#form-toast-error').toast('show');
            })
        } else {

            //UPDATE
            $scope.Student = {};
            $scope.Student.name = $scope.StudentName;
            $scope.Student.Birthday = $scope.StudentBirthday;
            $scope.Student.idCourse = $scope.StudentCourse;

            $scope.Student.IdStudentRegistrationNumber = document.getElementById("StudentID_").value;

            var content = JSON.stringify($scope.Student);
            $http({
                method: "post",
                url: "/Students/UpdateStudent/",
                datatype: "json",
                data: JSON.stringify($scope.Student)
            }).then(function (response) {
                $scope.GetAllDataStudents();
                //Clean Previous selections
                $scope.StudentName = "";
                $scope.StudentCourse = "";
                document.getElementById("inputBirthday").value = "";
                //Hide Modal
                $('#crud-modal-student').modal('hide');

                //TYPE OF TOAST
                var toastType = response.data.toString().substring(0, response.data.toString().indexOf("!"));
                if (toastType == "Sucess") {
                    $('#form-toast .toast-title').html("Sucess!");
                    $('#form-toast').css({ 'background-color': 'MediumSeaGreen', 'color': 'white' });
                    $('#toast-sucess-body').html(response.data)
                }
                else {
                    $('#form-toast .toast-title').html("Error!")
                    $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                    $('#toast-sucess-body').html(response.data)
                }
                $('#form-toast').toast('show');

                document.getElementById("btnSave-student").setAttribute("value", "Submit");
                document.getElementById("modal-title").innerHTML = "Add New Student";

                //INTERNAL ERROR
            }, function () {
                $('#form-toast .toast-title').html("Error!")
                $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
                $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                $('#form-toast-error').toast('show');
            })
        }
    }

    //POST DELETE
    $scope.DeleteStudent = function () {

        $scope.Student = {};
        $scope.Student.IdStudentRegistrationNumber = document.getElementById("StudentID_").value;

        $http({
            method: "post",
            url: '/Students/DeleteStudent/',
            datatype: "json",
            data: JSON.stringify($scope.Student)
        }).then(function (response) {
            //TYPE OF TOAST
            $('#delete-modal-student').modal('hide');

            var toastType = response.data.toString().substring(0, response.data.toString().indexOf("!"));
            if (toastType == "Sucess") {
                $('#form-toast .toast-title').html("Sucess!");
                $('#form-toast').css({ 'background-color': 'MediumSeaGreen', 'color': 'white' });
                $('#toast-sucess-body').html(response.data)
            }
            else {
                $('#form-toast .toast-title').html("Error!")
                $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
                $('#toast-sucess-body').html(response.data)
            }
            $('#form-toast').toast('show');

            $scope.GetAllDataStudents();

            //INTERNAL ERROR
        }, function () {
            $('#form-toast .toast-title').html("Error!")
            $('#toast-sucess-body').html("Internal Error Ocurred. Contact the administrator!")
            $('#form-toast').css({ 'background-color': 'Crimson', 'color': 'white' });
            $('#form-toast-error').toast('show');
        })

    }
});