using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Web;


public class OcrCommandLineCaller
{
    //This class was built using info from this SO question 
    //http://stackoverflow.com/questions/12925748/iapplicationactivationmanageractivateapplication-in-c
    public static void StartOcr(string fileName)
    {
        ApplicationActivationManager appActiveManager = new ApplicationActivationManager();//Class not registered
        uint pid;
        //The first arg in ActivateApplication is found in your registry at
        //HKEY_CURRENT_USER\Software\Classes\ActivatableClasses\Package\Some_Sort_Of_Guid\Server\App.App....\AppUserModelId
        appActiveManager.ActivateApplication("2ca0072b-e230-42c2-a5f2-6ee47ccce84d_yekwsnrkhg0pr!App", fileName, ActivateOptions.NoSplashScreen, out pid);
        System.Diagnostics.Process proc = null;
        foreach (var p in System.Diagnostics.Process.GetProcesses())
        {
            if (p.Id == pid)
            {
                proc = p;
            }
        }
        while (!proc.HasExited)
        {
            System.Threading.Thread.Sleep(100);
        }
    }
}
public enum ActivateOptions
{
    None = 0x00000000,  // No flags set
    DesignMode = 0x00000001,  // The application is being activated for design mode, and thus will not be able to
    // to create an immersive window. Window creation must be done by design tools which
    // load the necessary components by communicating with a designer-specified service on
    // the site chain established on the activation manager.  The splash screen normally
    // shown when an application is activated will also not appear.  Most activations
    // will not use this flag.
    NoErrorUI = 0x00000002,  // Do not show an error dialog if the app fails to activate.                                
    NoSplashScreen = 0x00000004,  // Do not show the splash screen when activating the app.
}

[ComImport, Guid("2e941141-7f97-4756-ba1d-9decde894a3d"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
interface IApplicationActivationManager
{
    // Activates the specified immersive application for the "Launch" contract, passing the provided arguments
    // string into the application.  Callers can obtain the process Id of the application instance fulfilling this contract.
    IntPtr ActivateApplication([In] String appUserModelId, [In] String arguments, [In] ActivateOptions options, [Out] out UInt32 processId);
    IntPtr ActivateForFile([In] String appUserModelId, [In] IntPtr /*IShellItemArray* */ itemArray, [In] String verb, [Out] out UInt32 processId);
    IntPtr ActivateForProtocol([In] String appUserModelId, [In] IntPtr /* IShellItemArray* */itemArray, [Out] out UInt32 processId);
}

[ComImport, Guid("45BA127D-10A8-46EA-8AB7-56EA9078943C")]//Application Activation Manager
class ApplicationActivationManager : IApplicationActivationManager
{
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)/*, PreserveSig*/]
    public extern IntPtr ActivateApplication([In] String appUserModelId, [In] String arguments, [In] ActivateOptions options, [Out] out UInt32 processId);
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern IntPtr ActivateForFile([In] String appUserModelId, [In] IntPtr /*IShellItemArray* */ itemArray, [In] String verb, [Out] out UInt32 processId);
    [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    public extern IntPtr ActivateForProtocol([In] String appUserModelId, [In] IntPtr /* IShellItemArray* */itemArray, [Out] out UInt32 processId);
}
