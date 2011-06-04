﻿#region license

// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using Castle.Services.Transaction.Tests.Framework;
using NUnit.Framework;

namespace Castle.Services.Transaction.Tests.TransactionManager
{
	public class TransactionDependent_CorrectBookKeeping_Specs : TransactionManager_SpecsBase
	{
		[Test]
		public void CurrentTransaction_Same_As_First_Transaction()
		{
			using (var t1 = Manager.CreateTransaction(new DefaultTransactionOptions()).Value.Transaction)
			{
				Assert.That(Manager.CurrentTransaction.Value, Is.SameAs(t1));
			}
		}

		[Test]
		public void CurrentTopTransaction_Same_As_First_Transaction()
		{
			using (var t1 = Manager.CreateTransaction(new DefaultTransactionOptions()).Value.Transaction)
			{
				Assert.That(Manager.CurrentTopTransaction.Value, Is.SameAs(t1));
			}
		}

		[Test]
		public void TopTransaction_AndTransaction_AreDifferent()
		{
			using (var t1 = Manager.CreateTransaction(new DefaultTransactionOptions()).Value.Transaction)
			{
				using (var t2 = Manager.CreateTransaction(new DefaultTransactionOptions()).Value.Transaction)
				{
					Assert.That(Manager.CurrentTopTransaction.Value, Is.SameAs(t1));
					Assert.That(Manager.CurrentTransaction.Value, Is.SameAs(t2));
					Assert.That(t1, Is.Not.SameAs(t2));
				}
			}
		}
	}
}