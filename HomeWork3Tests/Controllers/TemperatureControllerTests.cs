using NUnit.Framework;
using HomeWork3.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using HomeWork3.Interfaces;
using HomeWork3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Moq;

namespace HomeWork3.Controllers.Tests
{
    [TestFixture()]
    public class TemperatureControllerTests
    {
        [Test()]
        public void Index_Always_ReturnsView()
        {
            //Arrange
            Mock<ITemperatureValidator> validatorMock = new Mock<ITemperatureValidator>();
            var temperatureController = new TemperatureController(validatorMock.Object);
            //Act
            var result = temperatureController.Index();
            //Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test()]
        [TestCase(Enums.FileTypes.Txt)]
        [TestCase(Enums.FileTypes.Zip)]
        public void GenerateResultFile_IfTxtZipTypes_ReturnsFileContent(Enums.FileTypes fileType)
        {
            //Arrange
            Mock<ITemperatureValidator> validatorMock = new Mock<ITemperatureValidator>();
            var temperatureController = new TemperatureController(validatorMock.Object);
            //Act
            var result = temperatureController.GenerateResultFile("", fileType);
            //Assert
            Assert.IsInstanceOf<FileContentResult>(result);
        }

        [Test()]
        [TestCase(Enums.FileTypes.Bytes)]
        public void GenerateResultFile_IfBytesType_ReturnsFileContent(Enums.FileTypes fileType)
        {
            //Arrange
            Mock<ITemperatureValidator> validatorMock = new Mock<ITemperatureValidator>();
            var temperatureController = new TemperatureController(validatorMock.Object);
            //Act
            var result = temperatureController.GenerateResultFile("", fileType);
            //Assert
            Assert.IsInstanceOf<FileStreamResult>(result);
        }

        [Test()]
        [TestCase(Enums.FileTypes.Txt)]
        [TestCase(Enums.FileTypes.Zip)]
        [TestCase(Enums.FileTypes.None)]
        [TestCase(Enums.FileTypes.Bytes)]
        public void GetFTemperatureFromCTest_IfTemperatureIsNotValid_ReturnsBadRequest(Enums.FileTypes fileType)
        {
            //Arrange
            Mock<ITemperatureValidator> validatorMock = new Mock<ITemperatureValidator>();
            validatorMock.Setup(v => v.Validate(It.IsAny<double>()))
                .Returns(new ValidationResult() {ErrorMessage = "", Valid = false});
            var temperatureController = new TemperatureController(validatorMock.Object);
            //Act
            var result = temperatureController.GetFTemperatureFromC(0, fileType);
            //Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test()]
        public void GenerateZipArchive_Always_ReturnsFileContent()
        {
            //Arrange
            Mock<ITemperatureValidator> validatorMock = new Mock<ITemperatureValidator>();
            var temperatureController = new TemperatureController(validatorMock.Object);
            //Act
            var result = temperatureController.GenerateZipArchive("");
            //Assert
            Assert.IsInstanceOf<FileContentResult>(result);
        }
    }
}