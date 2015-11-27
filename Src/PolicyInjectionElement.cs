﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Unity.Configuration;

namespace Unity.InterceptionExtension.Configuration
{
    /// <summary>
    /// A shortcut element to enable the policy injection behavior.
    /// </summary>
    public class PolicyInjectionElement : InjectionMemberElement
    {
        /// <summary>
        /// Each element must have a unique key, which is generated by the subclasses.
        /// </summary>
        public override string Key
        {
            get { return "policyInjection"; }
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
        public override IEnumerable<InjectionMember> GetInjectionMembers(IUnityContainer container, Type fromType,
            Type toType, string name)
        {
            var behaviorElement = new InterceptionBehaviorElement
                {
                    TypeName = typeof(PolicyInjectionBehavior).AssemblyQualifiedName
                };

            return behaviorElement.GetInjectionMembers(container, fromType, toType, name);
        }
    }
}
