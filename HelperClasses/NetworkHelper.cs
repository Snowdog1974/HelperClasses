﻿using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Management;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SystemAndersson.Anet.BI.LoginSQL;

namespace HelperClasses
{
    public class Network
    {
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword,
            int dwLogonType, int dwLogonProvider, out SafeTokenHandle phToken);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public extern static bool CloseHandle(IntPtr handle);

        // Test harness. 
        // If you incorporate this code into a DLL, be sure to demand FullTrust.
        [PermissionSetAttribute(SecurityAction.Demand, Name = "FullTrust")]


        public static SqlConnection CreateImpersonateIntegratedConnection(Servers Conn, string server, string company, string user, string pwd)
        {
            SafeTokenHandle safeTokenHandle;
            SqlConnection result = new SqlConnection();
            try
            {
                string domainName;

                // Get the user token for the specified user, domain, and password using the 
                // unmanaged LogonUser method. 
                // The local machine name can be used for the domain name to impersonate a user on this machine.
                //label1.Content = "Enter the name of the domain on which to log on: ";
                domainName = GetDomain();



                const int LOGON32_PROVIDER_DEFAULT = 0;
                //This parameter causes LogonUser to create a primary token. 
                const int LOGON32_LOGON_INTERACTIVE = 2;

                // Call LogonUser to obtain a handle to an access token. 
                bool returnValue = LogonUser(user, domainName, pwd,
                    LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT,
                    out safeTokenHandle);

                //label1.Content = "LogonUser called.";

                if (false == returnValue)
                {
                    int ret = Marshal.GetLastWin32Error();
                    //Console.WriteLine("LogonUser failed with error code : {0}", ret);
                    throw new System.ComponentModel.Win32Exception(ret);
                }
                using (safeTokenHandle)
                {

                    // Use the token handle returned by LogonUser. 
                    using (WindowsIdentity newId = new WindowsIdentity(safeTokenHandle.DangerousGetHandle()))
                    {
                        using (WindowsImpersonationContext impersonatedUser = newId.Impersonate())
                        {

                            result = Conn.CreateConnectionIntegrated(server, company, false);


                        }
                    }
                    // Releasing the context object stops the impersonation 
                    // Check the identity.

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred. " + ex.Message);
            }
            return result;
        }


        public static string GetWorkGroup()
        {
            ManagementObject computer_system = new ManagementObject(
            string.Format(
            "Win32_ComputerSystem.Name='{0}'",
            Environment.MachineName));

            object result = computer_system["Workgroup"];
            return result.ToString();
        }


        public static string GetDomain()
        {
            ManagementObject computer_system = new ManagementObject(
            string.Format(
            "Win32_ComputerSystem.Name='{0}'",
            Environment.MachineName));

            object result = computer_system["domain"];
            return result.ToString();
        }

        public static bool CheckIfDomain()
        {
            string d = GetDomain();
            if (d == null || d == "")
            {
                return false;
            }
            else
            {
                return true;
            }


        }
    }
    public sealed class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private SafeTokenHandle()
            : base(true)
        {
        }

        [DllImport("kernel32.dll")]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr handle);

        protected override bool ReleaseHandle()
        {
            return CloseHandle(handle);
        }
    }
}
