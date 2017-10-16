using System;
using CoreGraphics;
using Foundation;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using SDWebImage;
using UIKit;

namespace WorkoutDemo.iOS
{
	[RegisterAttribute("BindableUrlUIImageView")]
	public class BindableUrlUIImageView : UIImageView
	{
		public UIImage PlaceHolderImage { get; set; }


		public BindableUrlUIImageView() : base()
		{
			Init();
		}

		public BindableUrlUIImageView(IntPtr ptr) : base(ptr)
		{
			Init();
		}

		public BindableUrlUIImageView(CGRect frame) : base(frame)
		{
			Init();
		}

		private void Init()
		{
			PlaceHolderImage = UIImage.FromBundle("ic_add_photo.png");
		}

		public event EventHandler UrlChanged;

		private string mImageUrl;
		public string ImageUrl
		{
			get { return mImageUrl; }
			set
			{
				mImageUrl = value;
				if (mLocalImage == null)
				{
					if (!string.IsNullOrEmpty(mImageUrl))
					{
						this.SetImage(new NSUrl(mImageUrl), null, SDWebImageOptions.RefreshCached);
						if (UrlChanged != null)
							UrlChanged(this, EventArgs.Empty);
					}
					else
					{
						if (PlaceHolderImage != null)
						{
							Image = PlaceHolderImage;
						}
					}
				}
			}
		}

		public event EventHandler LocalImageChanged;

		private UIImage mLocalImage;
		public UIImage LocalImage
		{
			get { return mLocalImage; }
			set
			{
				mLocalImage = value;
				if (mLocalImage != null)
				{
					Image = mLocalImage;
				}
				else
				{
					if (string.IsNullOrEmpty(mImageUrl))
					{
						if (PlaceHolderImage != null)
						{
							Image = PlaceHolderImage;
						}
					}
				}
			}
		}


		public event EventHandler LocalPathImageChanged;

		private string mLocalPathImage;
		public string LocalPathImage
		{
			get
			{
				return mLocalPathImage;
			}
			set
			{
				mLocalPathImage = value;
				if (mLocalPathImage != null)
				{
					Image = UIImage.FromFile(mLocalPathImage);
				}
				else
				{
					if (PlaceHolderImage != null)
					{
						Image = PlaceHolderImage;
					}
				}
			}
		}


	}

	public class BindableUrlUIImageViewTargetBinding : MvxTargetBinding
	{
		public BindableUrlUIImageViewTargetBinding(BindableUrlUIImageView target) : base(target)
		{
			target.UrlChanged += UrlChangedHandler;
			target.LocalImageChanged += LocalImageChangedHandler;
			target.LocalImageChanged += LocalPathImageChangedHandler;
		}

		private void UrlChangedHandler(object sender, EventArgs args)
		{
			var target = Target as BindableUrlUIImageView;
			if (target == null)
				return;

			var url = target.ImageUrl;
			FireValueChanged(url);
		}

		private void LocalImageChangedHandler(object sender, EventArgs args)
		{
			var target = Target as BindableUrlUIImageView;
			if (target == null)
				return;

			var image = target.LocalImage;
			FireValueChanged(image);
		}


		private void LocalPathImageChangedHandler(object sender, EventArgs args)
		{
			var target = Target as BindableUrlUIImageView;
			if (target == null)
				return;

			var image = target.LocalPathImage;
			FireValueChanged(image);
		}

		public override void SetValue(object value)
		{
			var target = Target as BindableUrlUIImageView;
			if (target == null)
				return;

			target.ImageUrl = (string)value;
		}

		public override Type TargetType
		{
			get
			{
				return typeof(string);
			}
		}


		public override MvxBindingMode DefaultMode
		{
			get
			{
				return MvxBindingMode.TwoWay;
			}
		}

		protected override void Dispose(bool isDisposing)
		{
			if (isDisposing)
			{
				var target = Target as BindableUrlUIImageView;
				if (target != null)
				{
					target.UrlChanged -= UrlChangedHandler;
					target.LocalImageChanged -= LocalImageChangedHandler;
					target.LocalPathImageChanged -= LocalPathImageChangedHandler;
				}
			}
			base.Dispose(isDisposing);
		}
	}
}
