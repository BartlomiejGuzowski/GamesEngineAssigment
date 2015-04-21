using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BGE
{
	class Lazer:MonoBehaviour
	{
        public void Update()
        {
            float speed = 5.0f;
            float width = 500;
            float height = 500;

		}
		public void drawLaser(GameObject user,GameObject Enemy){
			Debug.DrawLine (user.transform.position, Enemy.transform.position, Color.red, 2.0f); 
		}

	}


}

