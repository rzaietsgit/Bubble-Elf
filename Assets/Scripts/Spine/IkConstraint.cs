using System;

namespace Spine
{
	public class IkConstraint : IUpdatable
	{
		internal IkConstraintData data;

		internal ExposedList<Bone> bones = new ExposedList<Bone>();

		internal Bone target;

		internal float mix;

		internal int bendDirection;

		internal int level;

		public IkConstraintData Data => data;

		public ExposedList<Bone> Bones => bones;

		public Bone Target
		{
			get
			{
				return target;
			}
			set
			{
				target = value;
			}
		}

		public int BendDirection
		{
			get
			{
				return bendDirection;
			}
			set
			{
				bendDirection = value;
			}
		}

		public float Mix
		{
			get
			{
				return mix;
			}
			set
			{
				mix = value;
			}
		}

		public IkConstraint(IkConstraintData data, Skeleton skeleton)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data", "data cannot be null.");
			}
			if (skeleton == null)
			{
				throw new ArgumentNullException("skeleton", "skeleton cannot be null.");
			}
			this.data = data;
			mix = data.mix;
			bendDirection = data.bendDirection;
			bones = new ExposedList<Bone>(data.bones.Count);
			foreach (BoneData bone in data.bones)
			{
				bones.Add(skeleton.FindBone(bone.name));
			}
			target = skeleton.FindBone(data.target.name);
		}

		public void Update()
		{
			Apply();
		}

		public void Apply()
		{
			Bone bone = target;
			ExposedList<Bone> exposedList = bones;
			switch (exposedList.Count)
			{
			case 1:
				Apply(exposedList.Items[0], bone.worldX, bone.worldY, mix);
				break;
			case 2:
				Apply(exposedList.Items[0], exposedList.Items[1], bone.worldX, bone.worldY, bendDirection, mix);
				break;
			}
		}

		public override string ToString()
		{
			return data.name;
		}

		public static void Apply(Bone bone, float targetX, float targetY, float alpha)
		{
			Bone parent = bone.parent;
			float num = 1f / (parent.a * parent.d - parent.b * parent.c);
			float num2 = targetX - parent.worldX;
			float num3 = targetY - parent.worldY;
			float x = (num2 * parent.d - num3 * parent.b) * num - bone.x;
			float y = (num3 * parent.a - num2 * parent.c) * num - bone.y;
			float num4 = MathUtils.Atan2(y, x) * (180f / (float)Math.PI) - bone.shearX - bone.rotation;
			if (bone.scaleX < 0f)
			{
				num4 += 180f;
			}
			if (num4 > 180f)
			{
				num4 -= 360f;
			}
			else if (num4 < -180f)
			{
				num4 += 360f;
			}
			bone.UpdateWorldTransform(bone.x, bone.y, bone.rotation + num4 * alpha, bone.scaleX, bone.scaleY, bone.shearX, bone.shearY);
		}

		public static void Apply(Bone parent, Bone child, float targetX, float targetY, int bendDir, float alpha)
		{
			if (alpha == 0f)
			{
				child.UpdateWorldTransform();
				return;
			}
			float x = parent.x;
			float y = parent.y;
			float num = parent.scaleX;
			float num2 = parent.scaleY;
			float num3 = child.scaleX;
			int num4;
			int num5;
			if (num < 0f)
			{
				num = 0f - num;
				num4 = 180;
				num5 = -1;
			}
			else
			{
				num4 = 0;
				num5 = 1;
			}
			if (num2 < 0f)
			{
				num2 = 0f - num2;
				num5 = -num5;
			}
			int num6;
			if (num3 < 0f)
			{
				num3 = 0f - num3;
				num6 = 180;
			}
			else
			{
				num6 = 0;
			}
			float x2 = child.x;
			float a = parent.a;
			float b = parent.b;
			float c = parent.c;
			float d = parent.d;
			bool flag = Math.Abs(num - num2) <= 0.0001f;
			float num7;
			float num8;
			float num9;
			if (!flag)
			{
				num7 = 0f;
				num8 = a * x2 + parent.worldX;
				num9 = c * x2 + parent.worldY;
			}
			else
			{
				num7 = child.y;
				num8 = a * x2 + b * num7 + parent.worldX;
				num9 = c * x2 + d * num7 + parent.worldY;
			}
			Bone parent2 = parent.parent;
			a = parent2.a;
			b = parent2.b;
			c = parent2.c;
			d = parent2.d;
			float num10 = 1f / (a * d - b * c);
			float num11 = targetX - parent2.worldX;
			float num12 = targetY - parent2.worldY;
			float num13 = (num11 * d - num12 * b) * num10 - x;
			float num14 = (num12 * a - num11 * c) * num10 - y;
			num11 = num8 - parent2.worldX;
			num12 = num9 - parent2.worldY;
			float num15 = (num11 * d - num12 * b) * num10 - x;
			float num16 = (num12 * a - num11 * c) * num10 - y;
			float num17 = (float)Math.Sqrt(num15 * num15 + num16 * num16);
			float num18 = child.data.length * num3;
			float num21;
			float num20;
			if (flag)
			{
				num18 *= num;
				float num19 = (num13 * num13 + num14 * num14 - num17 * num17 - num18 * num18) / (2f * num17 * num18);
				if (num19 < -1f)
				{
					num19 = -1f;
				}
				else if (num19 > 1f)
				{
					num19 = 1f;
				}
				num20 = (float)Math.Acos(num19) * (float)bendDir;
				a = num17 + num18 * num19;
				b = num18 * MathUtils.Sin(num20);
				num21 = MathUtils.Atan2(num14 * a - num13 * b, num13 * a + num14 * b);
			}
			else
			{
				a = num * num18;
				b = num2 * num18;
				float num22 = a * a;
				float num23 = b * b;
				float num24 = num13 * num13 + num14 * num14;
				float num25 = MathUtils.Atan2(num14, num13);
				c = num23 * num17 * num17 + num22 * num24 - num22 * num23;
				float num26 = -2f * num23 * num17;
				float num27 = num23 - num22;
				d = num26 * num26 - 4f * num27 * c;
				if (d >= 0f)
				{
					float num28 = (float)Math.Sqrt(d);
					if (num26 < 0f)
					{
						num28 = 0f - num28;
					}
					num28 = (0f - (num26 + num28)) / 2f;
					float num29 = num28 / num27;
					float num30 = c / num28;
					float num31 = (!(Math.Abs(num29) < Math.Abs(num30))) ? num30 : num29;
					if (num31 * num31 <= num24)
					{
						num12 = (float)Math.Sqrt(num24 - num31 * num31) * (float)bendDir;
						num21 = num25 - MathUtils.Atan2(num12, num31);
						num20 = MathUtils.Atan2(num12 / num2, (num31 - num17) / num);
						goto IL_04fa;
					}
				}
				float num32 = 0f;
				float num33 = float.MaxValue;
				float x3 = 0f;
				float num34 = 0f;
				float num35 = 0f;
				float num36 = 0f;
				float x4 = 0f;
				float num37 = 0f;
				num11 = num17 + a;
				d = num11 * num11;
				if (d > num36)
				{
					num35 = 0f;
					num36 = d;
					x4 = num11;
				}
				num11 = num17 - a;
				d = num11 * num11;
				if (d < num33)
				{
					num32 = (float)Math.PI;
					num33 = d;
					x3 = num11;
				}
				float num38 = (float)Math.Acos((0f - a) * num17 / (num22 - num23));
				num11 = a * MathUtils.Cos(num38) + num17;
				num12 = b * MathUtils.Sin(num38);
				d = num11 * num11 + num12 * num12;
				if (d < num33)
				{
					num32 = num38;
					num33 = d;
					x3 = num11;
					num34 = num12;
				}
				if (d > num36)
				{
					num35 = num38;
					num36 = d;
					x4 = num11;
					num37 = num12;
				}
				if (num24 <= (num33 + num36) / 2f)
				{
					num21 = num25 - MathUtils.Atan2(num34 * (float)bendDir, x3);
					num20 = num32 * (float)bendDir;
				}
				else
				{
					num21 = num25 - MathUtils.Atan2(num37 * (float)bendDir, x4);
					num20 = num35 * (float)bendDir;
				}
			}
			goto IL_04fa;
			IL_04fa:
			float num39 = MathUtils.Atan2(num7, x2) * (float)num5;
			float rotation = parent.rotation;
			num21 = (num21 - num39) * (180f / (float)Math.PI) + (float)num4 - rotation;
			if (num21 > 180f)
			{
				num21 -= 360f;
			}
			else if (num21 < -180f)
			{
				num21 += 360f;
			}
			parent.UpdateWorldTransform(x, y, rotation + num21 * alpha, parent.scaleX, parent.scaleY, 0f, 0f);
			rotation = child.rotation;
			num20 = ((num20 + num39) * (180f / (float)Math.PI) - child.shearX) * (float)num5 + (float)num6 - rotation;
			if (num20 > 180f)
			{
				num20 -= 360f;
			}
			else if (num20 < -180f)
			{
				num20 += 360f;
			}
			child.UpdateWorldTransform(x2, num7, rotation + num20 * alpha, child.scaleX, child.scaleY, child.shearX, child.shearY);
		}
	}
}
