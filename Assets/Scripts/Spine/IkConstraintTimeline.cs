namespace Spine
{
	public class IkConstraintTimeline : CurveTimeline
	{
		public const int ENTRIES = 3;

		private const int PREV_TIME = -3;

		private const int PREV_MIX = -2;

		private const int PREV_BEND_DIRECTION = -1;

		private const int MIX = 1;

		private const int BEND_DIRECTION = 2;

		internal int ikConstraintIndex;

		internal float[] frames;

		public int IkConstraintIndex
		{
			get
			{
				return ikConstraintIndex;
			}
			set
			{
				ikConstraintIndex = value;
			}
		}

		public float[] Frames
		{
			get
			{
				return frames;
			}
			set
			{
				frames = value;
			}
		}

		public IkConstraintTimeline(int frameCount)
			: base(frameCount)
		{
			frames = new float[frameCount * 3];
		}

		public void SetFrame(int frameIndex, float time, float mix, int bendDirection)
		{
			frameIndex *= 3;
			frames[frameIndex] = time;
			frames[frameIndex + 1] = mix;
			frames[frameIndex + 2] = bendDirection;
		}

		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha)
		{
			float[] array = frames;
			if (!(time < array[0]))
			{
				IkConstraint ikConstraint = skeleton.ikConstraints.Items[ikConstraintIndex];
				if (time >= array[array.Length - 3])
				{
					ikConstraint.mix += (array[array.Length + -2] - ikConstraint.mix) * alpha;
					ikConstraint.bendDirection = (int)array[array.Length + -1];
					return;
				}
				int num = Animation.binarySearch(array, time, 3);
				float num2 = array[num + -2];
				float num3 = array[num];
				float curvePercent = GetCurvePercent(num / 3 - 1, 1f - (time - num3) / (array[num + -3] - num3));
				ikConstraint.mix += (num2 + (array[num + 1] - num2) * curvePercent - ikConstraint.mix) * alpha;
				ikConstraint.bendDirection = (int)array[num + -1];
			}
		}
	}
}
