using System;
using System.Collections.Generic;
using Assets.Scripts.Basic;
using Assets.Scripts.Mining.Controllers.GamePole;
using UnityEngine;

namespace Assets.Scripts.ViewDescription
{
	[CreateAssetMenu(fileName = "MiningSceneViewsDescription", menuName = "ScriptableObject/MiningSceneViewsDescription")]
	public class MiningSceneViewsDescription : ViewDescriptionsCollection<MiningSceneViewDescription>
	{
	}

	[Serializable]
	public class MiningSceneViewDescription : IIdentified
	{
		[SerializeField] private string _id;
		[SerializeField] private List<TypeBall> _balls;
		[SerializeField] private TypeBall _typeBallResource;
		[SerializeField] private float _percentageResources;
		[SerializeField] private TypeResources _typeResources;
		[SerializeField] private int _countResource;

		public string Id { get { return _id; } }
		public List<TypeBall> Balls { get { return _balls; } }
		public TypeBall TypeBallResource { get { return _typeBallResource; } }
		public float PercentageResources { get { return _percentageResources; } }
		public TypeResources TypeResources { get { return _typeResources; } }
		public int CountResource { get { return _countResource; } }
	}
}