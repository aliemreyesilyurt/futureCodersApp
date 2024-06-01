﻿namespace Services.Contracts
{
    public interface IServiceManager
    {
        ICourseService CourseService { get; }
        IBlogService BlogService { get; }
        IStepService StepService { get; }
        IUserStepService UserStepService { get; }
        IUserCourseService UserCourseService { get; }
        IReviewService ReviewService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}
