import { createBrowserRouter, Navigate, RouterProvider } from "react-router";
import HomePage, {loader as HomePageLoader} from "./pages/Home/Home.js";
import RootLayout, {loader as RootLoader} from "./pages/Root.js";
import ErrorPage from "./pages/Error.js";
import EmployerLandingPage from "./pages/EmployerLandingPage.js";
import LoginPage from "./pages/Authentication/LoginPage.js";
import { action as loginAction } from "./pages/Authentication/LoginPage.js";
import { AuthLayout } from "./pages/Authentication/AuthRoot.js";
import { Container } from "react-bootstrap";
import PreRegistrationPage from "./pages/Authentication/PreRegistrationPage.js";
import RegistrationPage from "./pages/Authentication/RegistrationPage.js";
import { action as registrationAction } from "./pages/Authentication/RegistrationPage.js";
import StudentRoot from "./pages/Student/StudentRoot.js";
import { loader as studentRootLoader } from "./pages/Student/StudentRoot.js";
import StudentProfileRoot from "./pages/Student/Profile/StudentProfileRoot.js"
import { StudentProfileDataPage } from "./pages/Student/Profile/StudentProfileDataPage.js";
import { action as studentProfileDataAction} from "./pages/Student/Profile/StudentProfileDataPage.js"
import { loader as studentProfileDataLoader } from "./pages/Student/Profile/StudentProfileDataPage.js";
import EmployerRoot from "./pages/Employer/EmployerRoot.js";
import { loader as EmployerRootLoader } from "./pages/Employer/EmployerRoot.js";
import CompleteEmployerProfiePage, {loader as CompleteEmployerProfiePageLoader} from "./pages/Employer/CompleteEmployerProfiePage.js";
import { action as CompleteEmployerProfileAction} from "./pages/Employer/CompleteEmployerProfiePage.js"
import EmployerProfilePage from "./pages/Employer/Profile/EmployerProfilePage.js";
import EmployerPersonalDataPage from "./pages/Employer/Profile/EmployerPersonalDataPage.js";
import { loader as EmployerPersonalDataLoader} from "./pages/Employer/Profile/EmployerPersonalDataPage.js";
import EmployerDataPage from "./pages/Employer/Profile/EmployerDataPage.js";
import { loader as EmployerDataLoader } from "./pages/Employer/Profile/EmployerDataPage.js";
import { action as EmployerDataAction} from "./pages/Employer/Profile/EmployerDataPage.js";
import { action as EmplyerPersonalDataAction} from "./pages/Employer/Profile/EmployerPersonalDataPage.js";
import AddInternshipPage from "./pages/Internship/AddInternshipPage.js";
import { action as AddInternshipAction, loader as AddInternshipLoader} from "./pages/Internship/AddInternshipPage.js";
import EmployerInternshipsPage, {loader as EmployerInternshipsPageLoader} from "./pages/Internship/EmployerInternshipsPage.js";
import EditInternshipPage, {loader as EditInternshipPageLoader, action as EditInternshipPageAction} from "./pages/Internship/EditInternshipPage.js";
import SearchInternshipsPage from "./pages/SearchInternships/SearchInternshipsPage.js";
import { QueryClientProvider } from "@tanstack/react-query";
import { queryClient } from "./util/reactQuery.js";
import {action as LogoutAction } from "./pages/Authentication/Logout.js"
import InternshipDetailPage, {loader as InternshipDetailLoader} from "./pages/Internship/InternshipDetailPage.js";
import InternshipsRoot, {loader as InternshipsRootLoader} from "./pages/SearchInternships/InternshipsRoot.js";
import InternshipApplyPage, {loader as InternshipApplyPageLoader, action as InternshipApplyPageAction} from "./pages/Internship/InternshipApplyPage.js";
import ApplicationsPage, {loader as ApplicationsPageLoader} from "./pages/Student/Applications/ApplicationsPage.js";
import ApplicationDetailPage, {loader as ApplicationDetailPageLoader} from "./pages/Student/Applications/ApplicationDetailPage.js";
import ApplySuccessPage from "./pages/Internship/ApplySuccessPage.js";
import ApplicantsList, {loader as ApplicantsListLoader} from "./pages/Employer/Applicants/ApplicantsList.js";
import LikedInternships, {loader as LikedInternshipsLoader} from "./pages/Student/Liked/LikedInternships.js";

const router = createBrowserRouter([
  {
    path: '/',
    element: <RootLayout/>,
    loader: RootLoader,
    errorElement: <ErrorPage/>,
    children: [
      {
        index: true,
        element: <HomePage/>,
        loader: HomePageLoader
      },
      {
        path: 'employerInfo',
        element: <EmployerLandingPage />
      }
    ]
  },
  {
    path: '/internships',
    id: "internships-root",
    element: <InternshipsRoot/>,
    loader: InternshipsRootLoader,
    children: [
      {
        index: true,
        element: <SearchInternshipsPage/>,
      },
      {
        path: ":internshipId",
        children: [
          {
            index: true,
            element: <InternshipDetailPage/>,
            loader: InternshipDetailLoader,
          },
          {
            path: "apply",
            children: [
              {
                index: true,
                element: <InternshipApplyPage/>,
                loader: InternshipApplyPageLoader,
                action: InternshipApplyPageAction
              },
              {
                path: "success",
                element: <ApplySuccessPage/>
              }
            ]
          }
        ]
      }
    ]
  },
  {
    path: '/auth',
    element: <AuthLayout/>,
    errorElement: <ErrorPage/>,
    children: [
      {
        index: true,
        element: <Navigate to='login' replace/>
      },
      {
        path: 'login',
        element: <LoginPage/>,
        action: loginAction
      },
      {
        path: 'registration',
        children: [
          {
            index: true,
            element: <PreRegistrationPage/>,
          },
          {
            path: ':role',
            element: <RegistrationPage/>,
            action: registrationAction,
          }
        ]
      }
    ]
  },
  {
    path: 'student',
    element: <StudentRoot/>,
    loader: studentRootLoader,
    children: [
      {
        index: true,
        element: <HomePage/>
      },
      {
        path: 'profile',
        element: <StudentProfileRoot/>,
        children: [
          {
            path: 'data',
            element: <StudentProfileDataPage/>,
            action: studentProfileDataAction,
            loader: studentProfileDataLoader
          },
          {
            path: 'liked',
            element: <LikedInternships/>,
            loader: LikedInternshipsLoader
          },
          {
            path: "applications",
            element: <ApplicationsPage/>,
            loader: ApplicationsPageLoader,
          },
          {
            path: "applications/:applicationId",
            element: <ApplicationDetailPage/>,
            loader: ApplicationDetailPageLoader
          }
        ]
      }
    ]
  },
  {
    path: 'employer',
    element: <EmployerRoot/>,
    loader: EmployerRootLoader,
    children: [
      {
        index: true,
        element: <h1 className="mt-3">Home Page</h1>
      },
      {
        path: 'profile',
        element: <EmployerProfilePage/>,
        children: [
          {
            path: 'my',
            element: <EmployerPersonalDataPage/>,
            loader: EmployerPersonalDataLoader,
            action: EmplyerPersonalDataAction
          },
          {
            path: 'company',
            element: <EmployerDataPage/>,
            loader: EmployerDataLoader,
            action: EmployerDataAction
          }
        ]
      },
      {
        path: 'internships',
        children: [
          {
            index: true,
            element: <EmployerInternshipsPage/>,
            loader: EmployerInternshipsPageLoader
          },
          {
            path: 'add',
            element: <AddInternshipPage/>,
            action: AddInternshipAction,
            loader: AddInternshipLoader
          },
          {
            path: ':internshipId',
            children: [
              {
                index:true,
                element: <EditInternshipPage/>,
                loader: EditInternshipPageLoader,
                action: EditInternshipPageAction
              },
              {
                path: 'applicants',
                element: <ApplicantsList/>,
                loader: ApplicantsListLoader
              }
            ]
          }
        ]
      }
    ]
  },
  {
    path: 'complete-profile',
    element: <CompleteEmployerProfiePage/>,
    action: CompleteEmployerProfileAction,
    loader: CompleteEmployerProfiePageLoader
  },
  {
    path: "logout",
    action: LogoutAction
  }
])



function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <Container>
        <RouterProvider router={router}/>
      </Container>
    </QueryClientProvider>
  );
}

export default App;
