﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.ComponentModel.DataAnnotations;

namespace PsychologyHealthCare.Repository.Models;

public partial class AppointmentTracking
{
    [Required(ErrorMessage = "ID is required.")]
    public string Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name must be between 2 and 100 characters.", MinimumLength = 2)]
    public string Name { get; set; }

    [Required(ErrorMessage = "Start time is required.")]
    [Range(0, long.MaxValue, ErrorMessage = "Start time must be a positive number.")]
    public long StartTime { get; set; }

    [Required(ErrorMessage = "End time is required.")]
    [Range(0, long.MaxValue, ErrorMessage = "End time must be a positive number.")]
    public long EndTime { get; set; }

    [Required(ErrorMessage = "Rating is required.")]
    public string Rating { get; set; }

    [Required(ErrorMessage = "Created date is required.")]
    public DateTime CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    [Required(ErrorMessage = "SystemStatus is required.")]
    public string SystemStatus { get; set; }

    [Required(ErrorMessage = "Holder is required.")]
    public string Holder { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Type is required.")]
    public string Type { get; set; }

    [Required(ErrorMessage = "Program Tracking ID is required.")]
    public string ProgramTrackingId { get; set; }

    public virtual ProgramTracking ProgramTracking { get; set; }

    public override string ToString()
    {
        return $"AppointmentTracking: [Id={Id}, Name={Name}, StartTime={StartTime}, EndTime={EndTime}, Rating={Rating}, " +
               $"CreatedDate={CreatedDate}, UpdatedDate={UpdatedDate}, SystemStatus={SystemStatus}, Holder={Holder}, " +
               $"Address={Address}, Type={Type}, ProgramTrackingId={ProgramTrackingId}]";
    }
}