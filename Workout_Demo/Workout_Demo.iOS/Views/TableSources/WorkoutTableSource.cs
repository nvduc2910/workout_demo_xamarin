
using System;
using Foundation;
using WorkoutDemo.Core.Models.ReturnModels;
using WorkoutDemo.iOS.Views.Cells.TableViewCells;
using MvvmCross.Binding.iOS.Views;
using UIKit;

namespace WorkoutDemo.iOS.Views.TableSources
{
    public class WorkoutTableSource : MvxTableViewSource
    {
        public WorkoutTableSource(UITableView tableView) : base(tableView)
        {
            tableView.RegisterNibForCellReuse(UINib.FromName("WorkoutTableViewCell", NSBundle.MainBundle), WorkoutTableViewCell.Key);
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
			WorkoutTableViewCell cell = (WorkoutTableViewCell)tableView.DequeueReusableCell(WorkoutTableViewCell.Key, indexPath);

			if (cell == null)
			{
				cell = WorkoutTableViewCell.Create();
			}

			cell.Model = item as Workout;

            cell.SelectionStyle = UITableViewCellSelectionStyle.None;

            cell.VAvatar.Layer.CornerRadius = 8f;
            cell.VAvatar.Layer.MasksToBounds = true;

            cell.BtnTick.Layer.BorderWidth = 1;
            cell.BtnTick.Layer.BorderColor = UIColor.White.CGColor;

            cell.BtnTick.Layer.CornerRadius = 10;
            cell.BtnTick.Layer.MasksToBounds = true;

            return cell;
        }

		public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
            return 100f;
		}
    }
}
