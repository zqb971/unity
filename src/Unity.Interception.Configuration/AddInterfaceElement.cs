﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;
using Unity.Configuration;
using Unity.Configuration.ConfigurationHelpers;
using Unity.Utility;

namespace Unity.InterceptionExtension.Configuration
{
    /// <summary>
    /// Configuration element that lets you specify additional interfaces
    /// to add when this type is intercepted.
    /// </summary>
    public class AddInterfaceElement : InjectionMemberElement
    {
        private const string TypeNamePropertyName = "type";

        /// <summary>
        /// Type of interface to add.
        /// </summary>
        [ConfigurationProperty(TypeNamePropertyName, IsRequired = true)]
        public string TypeName
        {
            get { return (string)base[TypeNamePropertyName]; }
            set { base[TypeNamePropertyName] = value; }
        }

        /// <summary>
        /// Each element must have a unique key, which is generated by the subclasses.
        /// </summary>
        public override string Key
        {
            get { return "addInterface: " + this.TypeName; }
        }

        /// <summary>
        /// Write the contents of this element to the given <see cref="XmlWriter"/>.
        /// </summary>
        /// <remarks>The caller of this method has already written the start element tag before
        /// calling this method, so deriving classes only need to write the element content, not
        /// the start or end tags.</remarks>
        /// <param name="writer">Writer to send XML content to.</param>
        public override void SerializeContent(XmlWriter writer)
        {
            Guard.ArgumentNotNull(writer, "writer");
            writer.WriteAttributeString(TypeNamePropertyName, this.TypeName);
        }

        /// <summary>
        /// Return the set of <see cref="InjectionMember"/>s that are needed
        /// to configure the container according to this configuration element.
        /// </summary>
        /// <param name="container">Container that is being configured.</param>
        /// <param name="fromType">Type that is being registered.</param>
        /// <param name="toType">Type that <paramref name="fromType"/> is being mapped to.</param>
        /// <param name="name">Name this registration is under.</param>
        /// <returns>One or more <see cref="InjectionMember"/> objects that should be
        /// applied to the container registration.</returns>
        public override IEnumerable<InjectionMember> GetInjectionMembers(IUnityContainer container, Type fromType, Type toType, string name)
        {
            Type interfaceType = TypeResolver.ResolveType(this.TypeName);
            return new[] { new AdditionalInterface(interfaceType) };
        }
    }
}
