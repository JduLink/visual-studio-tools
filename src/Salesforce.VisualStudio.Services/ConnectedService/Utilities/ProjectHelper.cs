﻿using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace Salesforce.VisualStudio.Services.ConnectedService.Utilities
{
    /// <summary>
    /// A utility class for working with VS projects (e.g. retrieving settings from).
    /// </summary>
    internal static class ProjectHelper
    {
        public static Project GetProjectFromHierarchy(IVsHierarchy projectHierarchy)
        {
            object projectObject;
            int result = projectHierarchy.GetProperty(
                VSConstants.VSITEMID_ROOT,
                (int)__VSHPROPID.VSHPROPID_ExtObject,
                out projectObject);
            ErrorHandler.ThrowOnFailure(result);
            return (Project)projectObject;
        }

        public static string GetCapabilities(IVsHierarchy projectHierarchy)
        {
            string capabilities = null;

            object capabilitiesObj;
            if (ErrorHandler.Succeeded(projectHierarchy.GetProperty(
                    (uint)VSConstants.VSITEMID.Root,
                    (int)__VSHPROPID5.VSHPROPID_ProjectCapabilities,
                    out capabilitiesObj)))
            {
                capabilities = capabilitiesObj as string;
            }

            return capabilities;
        }

        public static string GetProjectNamespace(Project project)
        {
            return project.Properties.Item("DefaultNamespace").Value.ToString();
        }
    }
}
