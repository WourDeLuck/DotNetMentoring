﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FileSystemService.Common.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Logging {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Logging() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FileSystemService.Common.Resources.Logging", typeof(Logging).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to New file has been found.
        /// </summary>
        public static string FileFound {
            get {
                return ResourceManager.GetString("FileFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File has been moved to the destination folder.
        /// </summary>
        public static string FileMoved {
            get {
                return ResourceManager.GetString("FileMoved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File has been renamed.
        /// </summary>
        public static string FileRenamed {
            get {
                return ResourceManager.GetString("FileRenamed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Initialize File System listener.
        /// </summary>
        public static string InitListener {
            get {
                return ResourceManager.GetString("InitListener", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Rule has been found that matches this file.
        /// </summary>
        public static string RuleFound {
            get {
                return ResourceManager.GetString("RuleFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No rule was found that would match the file.
        /// </summary>
        public static string RuleNotFound {
            get {
                return ResourceManager.GetString("RuleNotFound", resourceCulture);
            }
        }
    }
}
